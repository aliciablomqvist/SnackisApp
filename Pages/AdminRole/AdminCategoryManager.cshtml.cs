using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using SnackisApp.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisApp.Pages.AdminRole
{
    public class AdminCategoryManagerModel : PageModel
    {
        public UserManager<SnackisUser> _userManager;

        public List<SnackisUser> Users { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        [BindProperty]
        public Category NewCategory { get; set; }
        [BindProperty]
        public SubCategory NewSubCategory { get; set; }

        private readonly ApplicationDbContext _context;
        private readonly AdminHelper _adminHelper;

        public AdminCategoryManagerModel(ApplicationDbContext context, UserManager<SnackisUser> userManager, AdminHelper adminHelper)
        {
            _context = context;
            _userManager = userManager;
            _adminHelper = adminHelper;
        }

        public async Task<IActionResult> OnGetAsync(int? changeExistingCategoryId, int? deleteExistingCategoryId, int? changeExistingSubCategoryId, int? deleteExistingSubCategoryId)
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");

            Categories = await _context.Category.ToListAsync();
            SubCategories = await _context.SubCategory.ToListAsync();
            Users = await _userManager.Users.ToListAsync();

            if (changeExistingCategoryId.HasValue)
            {
                NewCategory = Categories.FirstOrDefault(c => c.Id == changeExistingCategoryId.Value);
            }
            if (deleteExistingCategoryId.HasValue)
            {
                await _adminHelper.DeleteCategory<Category>(deleteExistingCategoryId.Value);
                return RedirectToPage("./AdminCategoryManager");
            }

            if (changeExistingSubCategoryId.HasValue)
            {
                NewSubCategory = SubCategories.FirstOrDefault(c => c.Id == changeExistingSubCategoryId.Value);
            }

            if (deleteExistingSubCategoryId.HasValue)
            {
                await _adminHelper.DeleteCategory<SubCategory>(deleteExistingSubCategoryId.Value);
                return RedirectToPage("./AdminCategoryManager");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(NewCategory.Name))
            {
                await _adminHelper.SaveCategory(NewCategory);
            }

            return RedirectToPage("./AdminCategoryManager");
        }

        public async Task<IActionResult> OnPostSubCategoryAsync()
        {
            if (!string.IsNullOrEmpty(NewSubCategory.Name) && NewSubCategory.CategoryId != 0)
            {
                await _adminHelper.SaveSubCategory(NewSubCategory);
            }
            return RedirectToPage("./AdminCategoryManager");
        }
    }
}
