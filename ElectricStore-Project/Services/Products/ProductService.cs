using ElectricStore_Project.DTOs;
using ElectricStore_Project.DTOs.Products;
using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.Products;

namespace ElectricStore_Project.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDisplayDTO>> GetProductDisplayDTOsAsync()
        {

            var products =  await productRepository.GetAllProductsAsync();

            var productList = products.Select( p => new ProductDisplayDTO
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
            }).ToList();

            return productList;
        }

        public async Task<IEnumerable<ProductDisplayDTO>> GetAllProductByKeywordAsync(string keyword)
        {
            var products =  await productRepository.GetAllProductByKeyworkAsync(keyword);

            var productList = products.Select(p => new ProductDisplayDTO
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
            }).ToList();

            return productList;
        }
    }
}
