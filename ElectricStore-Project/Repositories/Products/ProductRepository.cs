using ElectricStore_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        ElectronicStoreContext _context;

        public ProductRepository(ElectronicStoreContext electronicStoreContext)
        {
            _context = electronicStoreContext;
        }
        public async Task<IEnumerable<ElectricStore_Project.Models.Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ElectricStore_Project.Models.Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<ElectricStore_Project.Models.Product>> GetAllProductByKeyworkAsync(string keywork)
        {
            return await _context.Products.Where(p => p.Name != null && p.Name.Contains(keywork)).ToListAsync();
        }
    }
}
