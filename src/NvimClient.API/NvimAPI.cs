using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MessagePack;
using NvimClient.NvimMsgpack;
using NvimClient.NvimMsgpack.Models;
using NvimClient.NvimProcess;

namespace NvimClient.API
{
  public partial class NvimAPI
  {
    public event EventHandler<NvimUnhandledRequestEventArgs> OnUnhandledRequest;
    public event EventHandler<NvimUnhandledNotificationEventArgs>
      OnUnhandledNotification;

    private readonly Stream _inputStream;
    private readonly Stream _outputStream;
    private readonly MessagePackStreamReader _streamReader;
    private readonly BlockingCollection<NvimMessage> _messageQueue;
    private readonly ConcurrentDictionary<long, PendingRequest>
      _pendingRequests;
    private delegate void NvimHandler(uint? requestId, dynamic[] arguments);
    private readonly ConcurrentDictionary<string, NvimHandler> _handlers;
    private uint _messageIdCounter;
    private readonly ManualResetEvent _waitEvent = new ManualResetEvent(false);

    /// <summary>
    /// Starts a new Nvim process and communicates
    /// with it through stdin and stdout streams.
    /// </summary>
    public NvimAPI() : this(Process.Start(
        new NvimProcessStartInfo(StartOption.Embed | StartOption.Headless)))
    {
    }

    /// <summary>
    /// Communicates with an already-running Nvim
    /// process through its stdin and stdout streams.
    /// </summary>
    /// <param name="process"></param>
    public NvimAPI(Process process) : this(process.StandardInput.BaseStream,
      process.StandardOutput.BaseStream)
    {
    }

    /// <summary>
    /// Communicates with Nvim through the specified server address.
    /// </summary>
    /// <param name="serverAddress"></param>
    public NvimAPI(string serverAddress) : this(
      GetStreamFromServerAddress(serverAddress))
    {
    }

    private static Stream GetStreamFromServerAddress(string serverAddress)
    {
      var lastColonIndex = serverAddress.LastIndexOf(':');
      if (lastColonIndex != -1 && lastColonIndex != 0
                               && int.TryParse(
                                    serverAddress.Substring(lastColonIndex + 1),
                                    out var port))
      {
        // TCP socket
        var tcpClient = new TcpClient();
        var hostname = serverAddress.Substring(0, lastColonIndex);
        tcpClient.Connect(hostname, port);
        return tcpClient.GetStream();
      }

      // Interprocess communication socket
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        // Named Pipe on Windows
        var match = Regex.Match(serverAddress,
          @"\\\\(?'serverName'[^\\]+)\\pipe\\(?'pipeName'[^\\]+)");
        var serverName = match.Groups["serverName"].Value;
        var pipeName   = match.Groups["pipeName"].Value;
        var pipeStream = new NamedPipeClientStream(serverName, pipeName,
          PipeDirection.InOut, PipeOptions.Asynchronous);
        pipeStream.Connect();
        return pipeStream;
      }

      // Unix Domain Socket on other OSes
      var unixDomainSocket = new Socket(AddressFamily.Unix,
        SocketType.Stream, ProtocolType.Unspecified);
      unixDomainSocket.Connect(new UnixDomainSocketEndPoint(serverAddress));
      return new NetworkStream(unixDomainSocket, true);
    }

    /// <summary>
    /// Communicates with Nvim through the provided stream.
    /// </summary>
    /// <param name="inputOutputStream">The stream to use.</param>
    public NvimAPI(Stream inputOutputStream) : this(inputOutputStream,
      inputOutputStream)
    {
    }

    /// <summary>
    /// Communicates with Nvim through the
    /// provided input and output streams.
    /// </summary>
    /// <param name="inputStream">The input stream to use.</param>
    /// <param name="outputStream">The output stream to use.</param>
    public NvimAPI(Stream inputStream, Stream outputStream)
    {
      _inputStream  = inputStream;
      _outputStream = outputStream;
      _streamReader = new MessagePackStreamReader(_outputStream);
      _messageQueue  = new BlockingCollection<NvimMessage>();
      _pendingRequests = new ConcurrentDictionary<long, PendingRequest>();
      _handlers = new ConcurrentDictionary<string, NvimHandler>();
      NvimMessageResolver.Register();

      StartSendLoop();
      StartReceiveLoop();
    }

    public void RegisterHandler(string name, Func<dynamic[], dynamic> handler) =>
      RegisterHandler(name, (requestId, args) =>
      {
        NvimResponse response = new NvimResponse();
        try
        {
          response.Result = handler(args);
        }
        catch (Exception exception)
        {
          response.Error = exception.ToString();
        }
        if (requestId.HasValue)
        {
          response.MessageId = (uint)requestId;
          _messageQueue.Add(response);
        }
      });

    public void RegisterHandler(string name, Action<dynamic[]> handler) =>
      RegisterHandler(name, (requestId, args) =>
      {
        NvimResponse response = new NvimResponse();
        try
        {
          handler(args);
        }
        catch (Exception exception)
        {
          response.Error = exception.ToString();
        }
        if (requestId.HasValue)
        {
          response.MessageId = (uint)requestId;
          _messageQueue.Add(response);
        }
      });

    public void RegisterHandler(string name,
      Func<dynamic[], Task<dynamic>> handler) => RegisterHandler(name,
      (requestId, args) =>
      {
        Task.Run(() =>
        {
          NvimResponse response = new NvimResponse();
          try
          {
            response.Result = handler(args);
          }
          catch (Exception exception)
          {
            response.Error = exception.ToString();
          }
          if (requestId.HasValue)
          {
            response.MessageId = (uint)requestId;
            _messageQueue.Add(response);
          }
        });
      });

    private void RegisterHandler(string name, NvimHandler handler)
    {
      if (!_handlers.TryAdd(name, handler))
      {
        throw new Exception(
          $"Handler for \"{name}\" is already registered");
      }
    }

    internal void SendResponse(NvimUnhandledRequestEventArgs args, object result,
      object error)
    {
      var response = new NvimResponse
                     {
                       MessageId = args.RequestId,
                       Result    = result,
                       Error     = error
                     };
      _messageQueue.Add(response);
    }

    private Task<NvimResponse> SendAndReceive(NvimRequest request)
    {
      request.MessageId = _messageIdCounter++;
      var pendingRequest = new PendingRequest();
      _pendingRequests[request.MessageId] = pendingRequest;
      _messageQueue.Add(request);
      return pendingRequest.GetResponse();
    }

    private Task<TResult> SendAndReceive<TResult>(NvimRequest request)
    {
      return SendAndReceive(request)
        .ContinueWith(task =>
        {
          var response = task.Result;
          return (TResult)response.Result;
        });
    }

    private void StartSendLoop()
    {
      Task.Run(async () =>
      {
        foreach (var request in _messageQueue.GetConsumingEnumerable())
        {
          await MessagePackSerializer.SerializeAsync<NvimMessage>(_inputStream, request);
          await _inputStream.FlushAsync();
        }
      }).ContinueWith(t => _waitEvent.Set());
    }

    private void StartReceiveLoop()
    {
      Receive();

      async void Receive()
      {
        NvimMessage message = null;
        try
        {
          CancellationToken cancellationToken;
          var bytes = await _streamReader.ReadAsync(cancellationToken);
          if (bytes is ReadOnlySequence<byte> msgpack)
          {
            message = MessagePackSerializer.Deserialize<NvimMessage>(msgpack);
          }
          else
          {
            throw new Exception("Failed to read from the stream");
          }
        }
        catch
        {
          _waitEvent.Set();
          throw;
        }

        switch (message)
        {
          case NvimNotification notification:
          {
            if (notification.Method == "redraw")
            {
              foreach (var uiEvent in notification.Arguments)
              {
                CallUIEventHandler(uiEvent[0], uiEvent[1]);
              }
            }

            var arguments = notification.Arguments;
            if (_handlers.TryGetValue(notification.Method, out var handler))
            {
              handler(null, arguments);
            }
            else
            {
              OnUnhandledNotification?.Invoke(this,
                new NvimUnhandledNotificationEventArgs(notification.Method,
                  arguments));
            }

            break;
          }
          case NvimRequest request:
          {
            dynamic arguments = request.Arguments;
            if (_handlers.TryGetValue(request.Method, out var handler))
            {
              handler(request.MessageId, arguments);
            }
            else
            {
              OnUnhandledRequest?.Invoke(this,
                new NvimUnhandledRequestEventArgs(this, request.MessageId,
                  request.Method, arguments));
            }

            break;
          }
          case NvimResponse response:
            if (!_pendingRequests.TryRemove(response.MessageId,
              out var pendingRequest))
            {
              throw new Exception(
                "Received response with "
                + $"unknown message ID \"{response.MessageId}\"");
            }

            pendingRequest.Complete(response);
            break;
          default:
            throw new TypeLoadException(
              $"Unknown message type \"{message.GetType()}\"");

        }

        Receive();
      }
    }

    public void WaitForDisconnect() => _waitEvent.WaitOne();

    private class PendingRequest
    {
      private readonly TimeSpan _responseTimeout = TimeSpan.FromSeconds(10);
      private readonly ManualResetEvent _receivedResponseEvent;
      private NvimResponse _response;          

      internal PendingRequest() =>
        _receivedResponseEvent = new ManualResetEvent(false);

      internal Task<NvimResponse> GetResponse()
      {
        var taskCompletionSource = new TaskCompletionSource<NvimResponse>();

        void RegisterResponseEvent(TimeSpan timeout) =>
          ThreadPool.RegisterWaitForSingleObject(_receivedResponseEvent,
            (state, timedOut) =>
            {
              if (timedOut)
              {
                Debug.WriteLine("Warning: response was not received "
                                + $"within {timeout.TotalSeconds} seconds");
                // Continue waiting without a timeout
                RegisterResponseEvent(Timeout.InfiniteTimeSpan);
              }
              else
              {
                taskCompletionSource.SetResult(
                  ((PendingRequest) state)._response);
              }
            },
            this, timeout, true);

        RegisterResponseEvent(_responseTimeout);

        return taskCompletionSource.Task;
      }

      internal void Complete(NvimResponse response)
      {
        _response = response;
        _receivedResponseEvent.Set();
      }
    }
  }
}
