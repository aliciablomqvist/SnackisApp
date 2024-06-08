using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Models;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class UserInfoModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;

        public UserInfoModel(UserManager<SnackisUser> userManager)
        {
            _userManager = userManager;
        }

        public SnackisUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            User = await _userManager.FindByIdAsync(userId);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}