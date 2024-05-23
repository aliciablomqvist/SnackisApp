using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SnackisApp.Models
{
    public class SnackisUser : IdentityUser
    {
  [Required]
            public string ProfileImageUrl { get; set; } = "default-profile-image-url.jpg";
    }
}
