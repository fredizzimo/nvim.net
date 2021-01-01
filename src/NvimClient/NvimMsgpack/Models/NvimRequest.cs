using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [NvimMessageType(0)]
  public class NvimRequest : NvimMessage
  {
    [Key(1)] public uint MessageId { get; set; }
    [Key(2)] public string Method { get; set; }
    //[Key(3)] public MessagePackObject Arguments { get; set; }
  }
}
