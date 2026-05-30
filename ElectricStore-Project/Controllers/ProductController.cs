using ElectricStore_Project.Services.Products;
using ElectricStore_Project.DTOs.Products;
using Microsoft.AspNetCore.Mvc;

namespace ElectricStore_Project.Controllers
{
    public class ProductController : Controller
    {
        IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Search(string? keyword)
        {
            if (keyword == null)
            {
                return View(new List<ProductDisplayDTO>());
            }

            ViewData["SearchQuery"] = keyword;
            var searchResults = await productService.GetAllProductByKeywordAsync(keyword);
            return View(searchResults);
        }

        public async Task<IActionResult> Filter(int categoryId)
        {

            if (categoryId <= 0)
            {
                return View(new List<ProductDisplayDTO>());
            }

            var products = await productService.GetProductsByCategoryIdAsync(categoryId);
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await productService.GetProductByIdAsync(id);
            if (p == null)
            {
                return NotFound();
            }

            var dto = new ProductDisplayDTO
            {
                Id = p.Id,
                Name = p.Name ?? "",
                Brand = p.Brand != null ? p.Brand.BrandName ?? "No Brand" : "No Brand",
                Category = p.Category != null ? p.Category.Name ?? "No Category" : "No Category",
                SalePrice = p.SalePrice ?? 0,
                OriginalPrice = p.OriginalPrice ?? 0,
                StockQuantity = p.StockQuantity ?? 0,
                SupplierName = p.Supplier != null ? p.Supplier.Name ?? "No Supplier" : "No Supplier",
                MadeIn = p.MadeInNavigation != null ? p.MadeInNavigation.Country1 ?? "Unknown" : "Unknown",
                SalceCount = p.SaleCount.HasValue ? p.SaleCount.Value.ToString() : "0",
                Rating = p.Rating ?? 0,
                GiftInfo = p.Gift ?? "",
                IstallmentTag = p.InstallmentTag ?? "",
                Description = p.Description ?? "",
                IsActive = p.IsActive ?? false,
                ImageList = p.ProductImages.OrderBy(pi => pi.DisplayOrder).Select(pi => pi.Img ?? "").ToList()
            };

            return View(dto);
        }
    }
}
