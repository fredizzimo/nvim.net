using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  public abstract class NvimTypeBase
  {
    [Key("id")]
    public int Id { get; set; }
  }
}
