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
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
