namespace NvimClient.NvimMsgpack.Models
{
  public class NvimRequest : NvimMessage
  {
    public uint MessageId { get; set; }
    public string Method { get; set; }
    public dynamic[] Arguments { get; set; }
  }
}
