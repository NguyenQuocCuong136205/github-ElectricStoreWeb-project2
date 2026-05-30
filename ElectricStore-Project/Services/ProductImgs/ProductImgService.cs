using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.ProductImgs;

namespace ElectricStore_Project.Services.ProductImgs
{
    public class ProductImgService : IProductImgService
    {
        IProductImageRepository productImageRepository;

        public ProductImgService(IProductImageRepository productImageRepository)
        {
            this.productImageRepository = productImageRepository;
        }

        public async Task<IEnumerable<ProductImage>> GetAllImageByProductIDAsync(int productID)
        {
            return await productImageRepository.GetAllImageByProductIDAsync(productID);
        }
    }
}
