using ElectricStore_Project.DTOs.Categories;
using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.Categories;

namespace ElectricStore_Project.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<CategoryResultDTO> CreateCategoryAsync(Category category)
        {
            var existing = await _categoryRepository.GetCategoryByNameAsync(category.Name);
            if (existing != null)
            {
                return new CategoryResultDTO
                {
                    IsSuccess = false,
                    Message = "Tên danh mục sản phẩm này đã tồn tại."
                };
            }

            if (await _categoryRepository.CreateCategoryAsync(category))
            {
                return new CategoryResultDTO
                {
                    IsSuccess = true,
                    Message = "Tạo danh mục sản phẩm thành công."
                };
            }

            return new CategoryResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình tạo danh mục sản phẩm."
            };
        }

        public async Task<CategoryResultDTO> EditCategoryAsync(Category category)
        {
            var existing = await _categoryRepository.GetCategoryByIdAsync(category.Id);
            if (existing == null)
            {
                return new CategoryResultDTO
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy danh mục cần chỉnh sửa."
                };
            }

            var duplicate = await _categoryRepository.GetCategoryByNameAsync(category.Name);
            if (duplicate != null && duplicate.Id != category.Id)
            {
                return new CategoryResultDTO
                {
                    IsSuccess = false,
                    Message = "Tên danh mục này đã được sử dụng bởi danh mục khác."
                };
            }

            if (await _categoryRepository.EditCategoryAsync(category))
            {
                return new CategoryResultDTO
                {
                    IsSuccess = true,
                    Message = "Cập nhật danh mục sản phẩm thành công."
                };
            }

            return new CategoryResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình cập nhật danh mục sản phẩm."
            };
        }

        public async Task<CategoryResultDTO> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return new CategoryResultDTO
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy danh mục cần xóa."
                };
            }

            if (category.Products != null && category.Products.Any())
            {
                return new CategoryResultDTO
                {
                    IsSuccess = false,
                    Message = "Không thể xóa danh mục này vì đang có sản phẩm liên kết."
                };
            }

            if (await _categoryRepository.DeleteCategoryAsync(id))
            {
                return new CategoryResultDTO
                {
                    IsSuccess = true,
                    Message = "Xóa danh mục sản phẩm thành công."
                };
            }

            return new CategoryResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình xóa danh mục sản phẩm."
            };
        }
    }
}
