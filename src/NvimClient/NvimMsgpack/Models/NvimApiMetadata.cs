using System.Collections.Generic;
using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [MessagePackObject()]
  public class NvimAPIMetadata
  {
    [Key("version")]
    public NvimVersion Version { get; set; }
    [Key("functions")]
    public List<NvimFunction> Functions { get; set; }
    [Key("ui_events")]
    public List<NvimUIEvent> UIEvents { get; set; }
    [Key("types")]
    public Dictionary<string, NvimType> Types { get; set; }
    [Key("error_types")]
    public Dictionary<string, NvimErrorType> ErrorTypes { get; set; }
  }
}
