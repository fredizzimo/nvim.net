using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using NvimClient.NvimMsgpack.Models;
using System.Text;
using System;

namespace NvimClient.NvimMsgpack
{
  class NvimMessageFormatter: IMessagePackFormatter<NvimMessage>
  {
    public NvimMessage Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      options.Security.DepthStep(ref reader);

      int count = reader.ReadArrayHeader();
      NvimMessage ret = null;
      if (count > 2)
      {
        int id = reader.ReadByte();
        var dynamicFormatter = options.Resolver.GetFormatter<dynamic>();
        var dynamicArrayFormatter = options.Resolver.GetFormatter<dynamic[]>();
        switch (id)
        {
          case 0:
            var req = new NvimRequest();
            req.MessageId = reader.ReadUInt32();
            req.Method = reader.ReadString();
            req.Arguments = dynamicFormatter.Deserialize(ref reader, options);
            ret = req;
            break;
          case 1:
            var resp = new NvimResponse();
            resp.MessageId = reader.ReadUInt32();
            resp.Error = dynamicFormatter.Deserialize(ref reader, options);
            resp.Result = dynamicFormatter.Deserialize(ref reader, options);
            ret = resp;
            break;
          case 2:
            var notif = new NvimNotification();
            notif.Method = reader.ReadString();
            notif.Arguments = dynamicArrayFormatter.Deserialize(ref reader, options);
            ret = notif;
            break;
          default:
            throw new ArgumentException("Unhandled message passed to the deserializer");
        }
      }
      reader.Depth--;
      return ret;
    }

    public void Serialize(ref MessagePackWriter writer, NvimMessage message, MessagePackSerializerOptions options)
    {
      switch (message)
      {
        case NvimRequest request:
          writer.WriteArrayHeader(4);
          writer.WriteInt8(0);
          writer.WriteUInt32(request.MessageId);
          var methodBytes = Encoding.UTF8.GetBytes(request.Method);
          writer.WriteString(methodBytes);
          var dynamicArrayFormatter = options.Resolver.GetFormatter<dynamic[]>();
          dynamicArrayFormatter.Serialize(ref writer, request.Arguments, options);
          break;
        case NvimResponse response:
          writer.WriteArrayHeader(4);
          writer.WriteInt8(1);
          writer.WriteUInt32(response.MessageId);
          var dynamicFormatter = options.Resolver.GetFormatter<object>();
          dynamicFormatter.Serialize(ref writer, (object)response.Error, options);
          dynamicFormatter.Serialize(ref writer, (object)response.Result, options);
          break;
        default:
          throw new ArgumentException("Unhandled message type passesd to the serializer");
      }
    }
  }
}
