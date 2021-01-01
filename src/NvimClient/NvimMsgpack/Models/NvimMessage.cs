using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  public abstract class NvimMessage
  {
    [Key(0)]
    public byte TypeId { get; set; }
  }
}
