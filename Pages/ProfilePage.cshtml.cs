using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Data;
using SnackisApp.Models;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class ProfilePageModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfilePageModel(UserManager<SnackisUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public IFormFile ProfileImage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (ProfileImage != null)
            {
                var fileName = $"{userId}_{ProfileImage.FileName}";
                var filePath = Path.Combine("wwwroot/profileImages", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                user.ProfileImageUrl = $"/profileImages/{fileName}";
                await _userManager.UpdateAsync(user);
            }

            return RedirectToPage("/Index");
        }
    }
}