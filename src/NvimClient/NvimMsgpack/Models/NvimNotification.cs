namespace NvimClient.NvimMsgpack.Models
{
  public class NvimNotification : NvimMessage
  {
    public string Method { get; set; }
    public dynamic[] Arguments { get; set; }
  }
}
