using ElectricStore_Project.DTOs.Products;
using ElectricStore_Project.Models;

namespace ElectricStore_Project.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDisplayDTO>> GetProductDisplayDTOsAsync();
        Task<IEnumerable<ProductDisplayDTO>> GetAllProductByKeywordAsync(string keyword);
    }
}
