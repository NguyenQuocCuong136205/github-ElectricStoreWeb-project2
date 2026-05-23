using ElectricStore_Project.DTOs.Categories;
using ElectricStore_Project.Models;

namespace ElectricStore_Project.Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<CategoryResultDTO> CreateCategoryAsync(Category category);
        Task<CategoryResultDTO> EditCategoryAsync(Category category);
        Task<CategoryResultDTO> DeleteCategoryAsync(int id);
    }
}
