using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SnackisApp.DTO
{
    public class UserRegistration
    {
        [Required]
        [Display(Name = "Pseudonym")]
        public string Pseudonym { get; set; }

        [Display(Name = "Bio")]
        public string Bio { get; set; }

       /* [Display(Name = "Profile Image URL")]
        public string ProfileImageUrl { get; set; }*/

         [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; } 

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
