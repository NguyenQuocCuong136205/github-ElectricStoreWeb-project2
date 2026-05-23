using ElectricStore_Project.DTOs.Products;

namespace ElectricStore_Project.Repositories.Products
{
    public interface IShowableProductInforDTORepository
    {
        Task<IEnumerable<ProductDisplayDTO>> GetAllProductsAsync();

        Task<ProductDisplayDTO?> GetProductByIdAsync(int id);
    }
}
