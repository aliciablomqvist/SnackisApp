using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisApp.Pages.AdminRole
{
    public class ViewReportsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewReportsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Report> Reports { get; set; }

        public async Task OnGetAsync()
        {
            Reports = await _context.Reports
                .Include(r => r.Post)
                .Include(r => r.ReportedBy)
                .ToListAsync();
       
// Nullcheck
        foreach (var report in Reports)
        {
            if (report.ReportedBy == null)
            {
                report.ReportedBy = new SnackisUser { UserName = "Unknown" };
            }
        }
    }
        public async Task<IActionResult> OnPostRejectAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            report.Status = ReportStatus.Rejected;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

       public async Task<IActionResult> OnPostDeletePostAsync(int postId)
{
    Console.WriteLine($"Attempting to delete post with ID: {postId}");

    var post = await _context.Post
        .FirstOrDefaultAsync(p => p.Id == postId);

    if (post == null)
    {
        Console.WriteLine($"No post found with ID: {postId}");
        return NotFound();
    }

    // Log to check post details
    Console.WriteLine($"Deleting Post ID: {post.Id}, Title: {post.Title}");

  

    // Deletes related reports
    var reports = await _context.Reports.Where(r => r.PostId == postId).ToListAsync();
    if (reports != null && reports.Any())
    {
        _context.Reports.RemoveRange(reports);
    }

    // Remove the post
    _context.Post.Remove(post);

    await _context.SaveChangesAsync();

    return RedirectToPage();
}
    }
}