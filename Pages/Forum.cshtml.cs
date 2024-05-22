using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SnackisApp.Models;
using SnackisApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;

namespace SnackisApp.Pages
{
    public class ForumModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ForumModel(ApplicationDbContext context)
        {
            _context = context;    
        }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public List<Post> Posts { get; set; }

        public async Task OnGetAsync(int deleteId)
        {
            if (deleteId != 0)
            {
                Post postToBeDeleted = await _context.Post.FindAsync(deleteId);

                if (postToBeDeleted != null)
                {
                    if (System.IO.File.Exists("./wwwroot/userImages/" + postToBeDeleted.Image))
                    {
                        System.IO.File.Delete("./wwwroot/userImages/" + postToBeDeleted.Image);
                    }
                    _context.Post.Remove(postToBeDeleted);
                    await _context.SaveChangesAsync();
                }
            }
            Posts = await _context.Post.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var image = UploadedImage;
            string fileName = "";

            if (image != null)
            {
                Random rnd = new();
                fileName = rnd.Next(0, 100000).ToString() + image.FileName;

                using (var fileStream = new FileStream("./wwwroot/userImages/" + fileName, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            Post.Date = DateTime.Now;
            Post.Image = fileName;
            Post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Post.Add(Post);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
