using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        public async Task OnGetAsync(int id)
        {
            Category = await _context.Category
                .Include(c => c.SubCategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            SubCategories = Category?.SubCategory;
        }
    }
}
