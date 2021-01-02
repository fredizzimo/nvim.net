
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;

namespace NvimClient.NvimMsgpack
{
  using ExtensionFactory = Func<int, object>;
  class NvimExtensionFormatter<T> : IMessagePackFormatter<T>
  {
    readonly private IMessagePackFormatter<T> _formatter;
    static readonly private Dictionary<int, ExtensionFactory> s_extensions = new Dictionary<int, ExtensionFactory>();

    static public void RegisterExtensions(IEnumerable<(int, Func<int, object>)> extensions)
    {
      foreach(var extension in extensions)
      {
        s_extensions.Add(extension.Item1, extension.Item2);
      }
    }

    public NvimExtensionFormatter()
    {
      var resolver = MessagePack.Resolvers.StandardResolver.Instance;
      _formatter = resolver.GetFormatter<T>();
    }
    public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
      MessagePackType type = reader.NextMessagePackType;
      if (type == MessagePackType.Extension)
      {
        var extensionReader = reader.CreatePeekReader();
        ExtensionHeader header;
        if (extensionReader.TryReadExtensionFormatHeader(out header))
        {
          // Negative extensions are reserved by message pack itself
          if (header.TypeCode >=0)
          {
            // Commit to read the extension
            options.Security.DepthStep(ref reader);
            reader.ReadExtensionFormatHeader();
            // All extensions in Neovim can be represented as plain integers
            int id = reader.ReadInt32();
            reader.Depth--;
            ExtensionFactory factory;
            if (s_extensions.TryGetValue(header.TypeCode, out factory))
            {
              return (T)factory(0);
            }
            return (T)(object)id;
          }
        }
      }

      return _formatter.Deserialize(ref reader, options);
    }

    public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
    {
      _formatter.Serialize(ref writer, value, options);
    }
  }
}
