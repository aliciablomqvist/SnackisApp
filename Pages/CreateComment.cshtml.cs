using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Data;
using SnackisApp.Models;
using System;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class CreateCommentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateCommentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comment NewComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int postId)
        {
            NewComment = new Comment
            {
                PostId = postId
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewComment.Date = DateTime.Now;

            _context.Comment.Add(NewComment);
            await _context.SaveChangesAsync();

            return RedirectToPage("CreatePost", new { id = NewComment.PostId });
        }
    }
}
