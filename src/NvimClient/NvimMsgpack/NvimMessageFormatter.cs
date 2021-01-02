using MessagePack;
using MessagePack.Formatters;
using NvimClient.NvimMsgpack.Models;
using System.Text;

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
        switch (id)
        {
          case 1:
            {
              var resp = new NvimResponse();
              resp.MessageId = reader.ReadUInt32();
              resp.Error = dynamicFormatter.Deserialize(ref reader, options);
              resp.Result = dynamicFormatter.Deserialize(ref reader, options);
              ret = resp;
            }
            break;
          default:
            break;
        }
      }
      reader.Depth--;
      return ret;
    }

    public void Serialize(ref MessagePackWriter writer, NvimMessage message, MessagePackSerializerOptions options)
    {
      var request = (NvimRequest)message;
      writer.WriteArrayHeader(4);
      writer.WriteInt8(0);
      writer.WriteUInt32(request.MessageId);
      var methodBytes = Encoding.UTF8.GetBytes(request.Method);
      writer.WriteString(methodBytes);
      var dynamicArrayFormatter = options.Resolver.GetFormatter<dynamic[]>();
      dynamicArrayFormatter.Serialize(ref writer, request.Arguments, options);
    }
  }
}
