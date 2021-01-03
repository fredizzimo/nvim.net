
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;

namespace NvimClient.NvimMsgpack
{
  using ExtensionFactory = Func<int, object>;

  public static class NvimExtensionRegistry
  {
    static readonly private Dictionary<int, ExtensionFactory> s_extensions = new Dictionary<int, ExtensionFactory>();
    static public void RegisterExtensions(IEnumerable<(int, ExtensionFactory)> extensions)
    {
      foreach(var extension in extensions)
      {
        s_extensions[extension.Item1] = extension.Item2;
      }
    }

    static public Dictionary<int, ExtensionFactory> Extensions
    {
      get
      {
        return s_extensions;
      }
    }
  }
  
  class NvimExtensionFormatter<T> : IMessagePackFormatter<T>
  {
    readonly private IMessagePackFormatter<T> _formatter;

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
            if (NvimExtensionRegistry.Extensions.TryGetValue(header.TypeCode, out factory))
            {
              return (T)factory(id);
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
