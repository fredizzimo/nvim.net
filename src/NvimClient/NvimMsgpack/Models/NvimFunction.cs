using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [MessagePackObject]
  public class NvimFunction : NvimFunctionEventBase
  {
    [Key("method")]
    public bool Method { get; set; }
    [Key("return_type")]
    public string ReturnType { get; set; }
  }
}
