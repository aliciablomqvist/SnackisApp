using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System;

namespace SnackisApp.Pages
{
    public class SubCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public SubCategory SubCategory { get; set; }
        [BindProperty]
        public Models.Post Post { get; set; }
        [BindProperty]
        public IFormFile UploadedImage { get; set; }
        public List<Models.Post> Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int? deleteId)
        {
            SubCategory = await _context.SubCategory
                .Include(sc => sc.Category)
                .FirstOrDefaultAsync(sc => sc.Id == id);


            if (SubCategory == null)
            {
                return NotFound();
            }

            if (deleteId.HasValue)
            {
                var postToBeDeleted = await _context.Post.FindAsync(deleteId.Value);

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

            Posts = await _context.Post
                .Include(p => p.User)
                .Where(p => p.SubCategoryId == id)
                .OrderByDescending(p => p.Date)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string fileName = "";
            if (UploadedImage != null)
            {
                Random rnd = new();
                fileName = rnd.Next(0, 100000).ToString() + Path.GetExtension(UploadedImage.FileName);

                var filePath = Path.Combine("./wwwroot/userImages/", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
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