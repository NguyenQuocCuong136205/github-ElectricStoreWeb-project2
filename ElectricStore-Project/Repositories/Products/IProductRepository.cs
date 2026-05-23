using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<ElectricStore_Project.Models.Product>> GetAllProductsAsync();

        Task<ElectricStore_Project.Models.Product> GetProductByIdAsync(int id);

        Task<IEnumerable<ElectricStore_Project.Models.Product>> GetAllProductByKeyworkAsync(string keywork);
    }
}
