using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SnackisApp.Models
{
    public class SnackisUser : IdentityUser
    {
        public string ProfileImageUrl { get; set; } = "./wwwroot/profileImages/yogaprofile.png";
        public string Pseudonym { get; set; }
        public string Bio { get; set; }
    }
}
