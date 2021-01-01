using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [MessagePackObject]
  public class NvimType : NvimTypeBase
  {
    [Key("prefix")]
    public string Prefix { get; set; }
  }
}
