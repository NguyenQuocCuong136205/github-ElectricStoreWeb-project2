using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<bool> CreateCategoryAsync(Category category);
        Task<bool> EditCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
