namespace NvimClient.NvimMsgpack.Models
{
  public class NvimResponse : NvimMessage
  {
    public uint MessageId { get; set; }
    public dynamic Error { get; set; }
    public dynamic Result { get; set; }
  }
}
