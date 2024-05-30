using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SnackisApp.Pages.AdminRole
{
    [Authorize(Roles = "Admin")]
    public class AdminPageModel : PageModel
    {
        public List<SnackisUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
        [BindProperty(SupportsGet = true)] public string RoleName { get; set; }
        [BindProperty(SupportsGet = true)] public string AddUserId { get; set; }
        [BindProperty(SupportsGet = true)] public string RemoveUserId { get; set; }

        public readonly UserManager<SnackisUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;

        public AdminPageModel(RoleManager<IdentityRole> roleManager, UserManager<SnackisUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            Users = _userManager.Users.ToList();
            Roles = _roleManager.Roles.OrderByDescending(x => x.Id).ToList();

            if (AddUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                if (alterUser != null && !string.IsNullOrEmpty(RoleName))
                {
                    await _userManager.AddToRoleAsync(alterUser, RoleName);
                }
            }
            if (RemoveUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                if (alterUser != null && !string.IsNullOrEmpty(RoleName))
                {
                    await _userManager.RemoveFromRoleAsync(alterUser, RoleName);
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(RoleName))
            {
                await CreateRole(RoleName);
            }

            return RedirectToPage("./Index");
        }

        public async Task CreateRole(string roleName)
        {
            bool roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                var role = new IdentityRole
                {
                    Name = roleName,
                };

                await _roleManager.CreateAsync(role);
            }
        }
    }
}
