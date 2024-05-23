using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class ForumModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SnackisUser> _userManager;

        public ForumModel(ApplicationDbContext context, UserManager<SnackisUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Post> Posts { get; set; }

        public async Task OnGetAsync()
        {
            Posts = await _context.Post
                .Include(p => p.User) // Inkludera användarrelaterade data
                .ToListAsync();
        }
    }
}
