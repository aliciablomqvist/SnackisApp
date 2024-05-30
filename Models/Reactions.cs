using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackisApp.Models
{
    public class Reaction
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }

     public string ReactedbyId { get; set; }
    public SnackisUser Reactedby { get; set; }
    public string ReactionType { get; set; }
    public DateTime CreatedAt { get; set; }
}
}
