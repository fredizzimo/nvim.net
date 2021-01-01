using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [NvimMessageType(1)]
  public class NvimResponse : NvimMessage
  {
    [Key(1)] public uint MessageId { get; set; }
    [Key(2)] public dynamic Error { get; set; }
    [Key(3)] public dynamic Result { get; set; }
  }
}
