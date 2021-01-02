using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  public class NvimResponse : NvimMessage
  {
    [Key(0)] public uint MessageId { get; set; }
    [Key(1)] public dynamic Error { get; set; }
    [Key(2)] public dynamic Result { get; set; }
  }
}
