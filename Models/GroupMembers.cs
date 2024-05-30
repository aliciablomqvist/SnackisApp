namespace SnackisApp.Models
{
    public class GroupMember
{
    public int GroupId { get; set; }
    public Group Group { get; set; }

    public string UserId { get; set; }
    public SnackisUser User { get; set; }
}
}
