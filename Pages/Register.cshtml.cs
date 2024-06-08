using System.IO;
using System.Threading.Tasks;
using SnackisApp.Models;
using SnackisApp.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;

namespace SnackisApp.Pages
{
    public class RegisterPageModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SignInManager<SnackisUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegisterPageModel(UserManager<SnackisUser> userManager, SignInManager<SnackisUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public UserRegistration Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (Input.ProfileImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profileImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Input.ProfileImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Input.ProfileImage.CopyToAsync(fileStream);
                    }
                }
                else
                {

                    uniqueFileName = "default-profile.png";
                }
                var user = new SnackisUser
                {
                    Pseudonym = Input.Pseudonym,
                    Bio = Input.Bio,
                    ProfileImageUrl = "/profileImages/" + uniqueFileName,
                    PhoneNumber = Input.PhoneNumber,
                    UserName = Input.Email,
                    Email = Input.Email,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}