namespace SnackisApp.Models
{
    public class GroupInvitation
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }

    public string InvitedUserId { get; set; }
    public SnackisUser InvitedUser { get; set; }

    public string InvitingUserId { get; set; }
    public SnackisUser InvitingUser { get; set; }

    public DateTime Timestamp { get; set; }
    public bool Accepted { get; set; }
}
}