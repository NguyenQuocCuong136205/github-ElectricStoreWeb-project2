using ElectricStore_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ElectronicStoreContext _context;

        public CategoryRepository(ElectronicStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.SingleOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditCategoryAsync(Category category)
        {
            try
            {
                var existing = await _context.Categories.FindAsync(category.Id);
                if (existing == null) return false;
                existing.Name = category.Name;
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null) return false;
                _context.Categories.Remove(category);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
