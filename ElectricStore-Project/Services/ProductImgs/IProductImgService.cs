using ElectricStore_Project.Models;

namespace ElectricStore_Project.Services.ProductImgs
{
    public interface IProductImgService
    {
        Task<IEnumerable<ProductImage>> GetAllImageByProductIDAsync(int productID);
    }
}
