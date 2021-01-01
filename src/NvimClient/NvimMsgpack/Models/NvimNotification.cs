using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [NvimMessageType(2)]
  public class NvimNotification : NvimMessage
  {
    [Key(1)] public string Method { get; set; }
    //[Key(2)] public MessagePackObject Arguments { get; set; }
  }
}
