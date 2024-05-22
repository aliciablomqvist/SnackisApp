using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TestCategoriesModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public TestCategoriesModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Category> Categories { get; set; }

    public async Task OnGetAsync()
    {
        Categories = await _context.Category
            .Include(c => c.SubCategory)
            .AsNoTracking()
            .ToListAsync();
    }
}
