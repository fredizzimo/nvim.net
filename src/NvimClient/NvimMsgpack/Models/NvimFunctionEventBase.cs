using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  public class NvimFunctionEventBase
  {
    [Key("name")]
    public string Name { get; set; }
    [Key("parameters")]
    public NvimParameter[] Parameters { get; set; }
    [Key("since")]
    public int Since { get; set; }
    [Key("deprecated_since")]
    public int? DeprecatedSince { get; set; }
  }
}
