using SnackisApp.Models;
using SnackisApp.Data;

namespace SnackisApp.Helpers
{
    public class AdminHelper
    {
        private readonly ApplicationDbContext _context;

        public AdminHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        // Save or update a category
        public async Task SaveCategory(Category newCategory)
        {
            var existingCategory = _context.Category.FirstOrDefault(c => c.Id == newCategory.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = newCategory.Name;
            }
            else
            {
                _context.Add(newCategory);
            }
            await _context.SaveChangesAsync();
        }

        // Save or update a subcategory
        public async Task SaveSubCategory(SubCategory newSubCategory)
        {
            var existingSubCategory = _context.SubCategory.FirstOrDefault(c => c.Id == newSubCategory.Id);

            if (existingSubCategory != null)
            {
                existingSubCategory.Name = newSubCategory.Name;
                existingSubCategory.CategoryId = newSubCategory.CategoryId;
            }
            else
            {
                _context.Add(newSubCategory);
            }
            await _context.SaveChangesAsync();
        }

        // Delete a category or subcategory by ID
        public async Task DeleteCategory<T>(int id) where T : class
        {
            var category = await _context.Set<T>().FindAsync(id);

            if (category != null)
            {
                _context.Set<T>().Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}