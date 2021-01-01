using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [MessagePackObject]
  public class NvimVersion
  {
    [Key("major")]
    public int Major { get; set; }
    [Key("minor")]
    public int Minor { get; set; }
    [Key("patch")]
    public int Patch { get; set; }
    [Key("api_level")]
    public int ApiLevel { get; set; }
    [Key("api_compatible")]
    public int ApiCompatible { get; set; }
    [Key("api_prerelease")]
    public bool ApiPrerelease { get; set; }
  }
}
