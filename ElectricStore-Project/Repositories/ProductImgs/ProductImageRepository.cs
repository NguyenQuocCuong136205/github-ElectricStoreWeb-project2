using ElectricStore_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.ProductImgs
{
    public class ProductImageRepository : IProductImageRepository   
    {
        ElectronicStoreContext _context;

        public ProductImageRepository(ElectronicStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImage>> GetAllImageByProductIDAsync(int id)
        {
            var imgs =  await _context.ProductImages.Where(p => p.ProductId == id).ToListAsync();
            return imgs;
        }
    }
}
