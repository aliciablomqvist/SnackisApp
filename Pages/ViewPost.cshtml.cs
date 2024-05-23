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

        [BindProperty]
        public int? ParentCommentId { get; set; }

        [BindProperty]
        public string NewCommentContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await _context.Post.FirstOrDefaultAsync(p => p.Id == id);
            if (Post == null)
            {
                return NotFound();
            }

            Comments = await _context.Comment
                .Include(c => c.Replies)
                .Where(c => c.PostId == id && c.ParentCommentId == null)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Post = await _context.Post.FirstOrDefaultAsync(p => p.Id == id);
            if (Post == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(NewCommentContent))
            {
                ModelState.AddModelError("NewCommentContent", "Content is required.");
            }

            // Remove navigational properties from ModelState
            ModelState.Clear();

            NewComment = new Comment
            {
                PostId = Post.Id,
                ParentCommentId = ParentCommentId,
                Content = NewCommentContent,
                Date = DateTime.Now
            };

            if (!ModelState.IsValid)
            {
                Comments = await _context.Comment
                    .Include(c => c.Replies)
                    .Where(c => c.PostId == id && c.ParentCommentId == null)
                    .ToListAsync();
                return Page();
            }

            _context.Comment.Add(NewComment);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = id });
        }
    }
}
