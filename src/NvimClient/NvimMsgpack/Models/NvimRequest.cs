using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [MessagePackObject]
  public class NvimRequest : NvimMessage
  {
    [Key(0)] public uint MessageId { get; set; }
    [Key(1)] public string Method { get; set; }
    [Key(2)] public dynamic[] Arguments { get; set; }
  }
}
