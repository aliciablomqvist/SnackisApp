using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using SnackisApp.Models;
using SnackisApp.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace SnackisApp.Pages
{
    public class CreatePostModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public CreatePostModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Post Post { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public List<Models.Post> Posts { get; set; }

        public async Task OnGetAsync(int deleteId)
        {
            if (deleteId != 0)
            {
                Models.Post postToBeDeleted = await _context.Post.FindAsync(deleteId);

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

            ViewData["Categories"] = new SelectList(await _context.Category.ToListAsync(), "Id", "Name");
            ViewData["SubCategories"] = new SelectList(await _context.SubCategory.ToListAsync(), "Id", "Name");

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

            var categoryExists = await _context.Category.AnyAsync(c => c.Id == Post.CategoryId);
            var subCategoryExists = await _context.SubCategory.AnyAsync(sc => sc.Id == Post.SubCategoryId);

            if (!categoryExists || !subCategoryExists)
            {
                ModelState.AddModelError("", "Invalid CategoryId or SubCategoryId.");
                return Page();
            }

            _context.Post.Add(Post);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes: " + ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}