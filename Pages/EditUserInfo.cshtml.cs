using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Models;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class EditUserInfoModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;

        public EditUserInfoModel(UserManager<SnackisUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Pseudonym { get; set; }
            public string Bio { get; set; }
            public IFormFile ProfileImage { get; set; }
        }

        public SnackisUser UserEntity { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Correct usage
            UserEntity = await _userManager.FindByIdAsync(userId);

            if (UserEntity == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                Pseudonym = UserEntity.Pseudonym,
                Bio = UserEntity.Bio
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Correct usage
            UserEntity = await _userManager.FindByIdAsync(userId);

            if (UserEntity == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            UserEntity.Pseudonym = Input.Pseudonym;
            UserEntity.Bio = Input.Bio;

            if (Input.ProfileImage != null)
            {
                var filePath = Path.Combine("./wwwroot/profileImages", Input.ProfileImage.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Input.ProfileImage.CopyToAsync(stream);
                }

                UserEntity.ProfileImageUrl = "/profileImages/" + Input.ProfileImage.FileName;
            }

            var result = await _userManager.UpdateAsync(UserEntity);

            if (result.Succeeded)
            {
                return RedirectToPage("/UserInfo", new { userId = userId });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
