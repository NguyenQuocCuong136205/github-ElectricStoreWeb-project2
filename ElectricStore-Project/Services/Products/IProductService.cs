using ElectricStore_Project.DTOs.Products;

namespace ElectricStore_Project.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDisplayDTO>> GetProductDisplayDTOsAsync();

        Task<IEnumerable<ProductDisplayDTO>> GetAllProductByKeywordAsync(string keyword);
    }
}
