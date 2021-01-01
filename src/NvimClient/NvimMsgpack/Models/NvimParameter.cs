using MessagePack;

namespace NvimClient.NvimMsgpack.Models
{
  [MessagePackObject]
  public class NvimParameter
  {
    [Key(0)] public string Type { get; set; }
    [Key(1)] public string Name { get; set; }
  }
}
