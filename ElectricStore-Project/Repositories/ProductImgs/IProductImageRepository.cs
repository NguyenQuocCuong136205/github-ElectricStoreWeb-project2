using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.ProductImgs
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetAllImageByProductIDAsync(int id);
    }
}
