using ElectricStore_Project.DTOs.Products;
using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.Product
{
    // admin sẽ dùng repository này để lấy toàn bộ thông tin về sản phẩm, bao gồm các thông tin mật
    public class ShowableProductInforRepository : IShowableProductInforDTORepository
    {
        ElectronicStoreContext _context;

        public ShowableProductInforRepository(ElectronicStoreContext electronicStoreContext){
            _context = electronicStoreContext;
        }
        public async Task<IEnumerable<ProductDisplayDTO>> GetAllProductsAsync()
        {
            var allBooks = await _context.Products.ToListAsync();
            
            var productList = allBooks.Select(p => new ProductDisplayDTO
            {
                Name = p.Name ?? "",
                Brand = p.Brand != null ? p.Brand.BrandName ?? "No Brand" : "No Brand",
                Category = p.Category != null ? p.Category.Name ?? "No Category" : "No Category",
                SalePrice = p.SalePrice ?? 0,
                StockQuantity = p.StockQuantity ?? 0,
                MadeIn = p.MadeInNavigation != null ? p.MadeInNavigation.Country1 ?? "Unknown" : "Unknown",
                Rating = p.Rating ?? 0,
                Description = p.Description ?? "",
                IsActive = p.IsActive ?? false
            });

            return productList;
        }

        public async Task<ProductDisplayDTO?> GetProductByIdAsync(int id)
        {
            var book = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return new ProductDisplayDTO
            {
                Name = book?.Name ?? "",
                Brand = book?.Brand != null ? book.Brand.BrandName ?? "No Brand" : "No Brand",
                Category = book?.Category != null ? book.Category.Name ?? "No Category" : "No Category",
                SalePrice = book?.SalePrice ?? 0,
                StockQuantity = book?.StockQuantity ?? 0,
                MadeIn = book?.MadeInNavigation != null ? book.MadeInNavigation.Country1 ?? "Unknown" : "Unknown",
                Rating = book?.Rating ?? 0,
                Description = book?.Description ?? "",
                IsActive = book?.IsActive ?? false
            };
        }
    }
}
