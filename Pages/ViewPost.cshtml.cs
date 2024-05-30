using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Helpers;
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
    private readonly UserManager<SnackisUser> _userManager;

    public ViewPostModel(ApplicationDbContext context, UserManager<SnackisUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public Post Post { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Reaction> Reactions { get; set; } // Add this line

    [BindProperty]
    public Comment NewComment { get; set; }
    [BindProperty]
    public int? ParentCommentId { get; set; }
    [BindProperty]
    public string NewCommentContent { get; set; }
    public string UserName { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Post = await _context.Post
            .Include(p => p.User) // Include user to get the username and profile image
            .Include(p => p.Reactions) // Include reactions
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Post == null)
        {
            return NotFound();
        }

        Comments = await _context.Comment
            .Include(c => c.Replies)
            .Where(c => c.PostId == id && c.ParentCommentId == null)
            .ToListAsync();

        Reactions = Post.Reactions.ToList(); // Populate Reactions list
        UserName = Post.User.UserName;

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

        ModelState.Clear();

        NewCommentContent = WordFilter.FilterInappropriateWords(NewCommentContent);

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

    public async Task<IActionResult> OnPostReportAsync(int postId, string reportedById)
    {
        var post = await _context.Post.FindAsync(postId);
        if (post == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByNameAsync(reportedById);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid user.");
            return Page();
        }

        var report = new Report
        {
            PostId = postId,
            ReportedById = user.Id,
            ReportDate = DateTime.Now,
            Reason = "Inappropriate content"
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return RedirectToPage(new { id = postId });
    }
}
}