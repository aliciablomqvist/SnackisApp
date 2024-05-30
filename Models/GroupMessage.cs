namespace SnackisApp.Models
{
  public class GroupMessage
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }

    public string UserId { get; set; }
    public SnackisUser User { get; set; }

    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}
}
