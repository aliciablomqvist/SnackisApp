using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class ViewPostModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewPostModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }

        [BindProperty]
        public Comment NewComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await _context.Post.FirstOrDefaultAsync(p => p.Id == id);
            if (Post == null)
            {
                return NotFound();
            }

            Comments = await _context.Comment.Where(c => c.PostId == id).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Post = await _context.Post.FirstOrDefaultAsync(p => p.Id == id);
            if (Post == null)
            {
                return NotFound();
            }

            NewComment.PostId = Post.Id;
            NewComment.Date = DateTime.Now;

            // Remove the Post property from the ModelState before validating the model
            ModelState.Remove("NewComment.Post");

            if (!ModelState.IsValid)
            {
                Comments = await _context.Comment.Where(c => c.PostId == id).ToListAsync();
                Console.WriteLine($"ModelState is invalid. Errors: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
                return Page();
            }

            _context.Comment.Add(NewComment);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = id });
        }
    }
}
