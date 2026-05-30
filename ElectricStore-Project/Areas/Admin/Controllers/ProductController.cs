    using ElectricStore_Project.Models;
using ElectricStore_Project.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ElectronicStoreContext _context;

        public ProductController(IProductService productService, ElectronicStoreContext context)
        {
            this.productService = productService;
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm.";
                return RedirectToAction(nameof(Index));
            }

            // Load related lists for dropdowns in Edit Modal
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Brands = await _context.Brands.ToListAsync();
            ViewBag.Countries = await _context.Countries.ToListAsync();
            ViewBag.Suppliers = await _context.Suppliers.ToListAsync();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (product == null)
            {
                TempData["ErrorMessage"] = "Dữ liệu chỉnh sửa không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var existingProduct = await _context.Products.FindAsync(product.Id);
                if (existingProduct == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy sản phẩm cần cập nhật.";
                    return RedirectToAction(nameof(Index));
                }

                // Update editable fields
                existingProduct.Name = product.Name;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.BrandId = product.BrandId;
                existingProduct.ImportPrice = product.ImportPrice;
                existingProduct.SalePrice = product.SalePrice;
                existingProduct.OriginalPrice = product.OriginalPrice;
                existingProduct.MadeIn = product.MadeIn;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.IsActive = product.IsActive;
                existingProduct.SupplierId = product.SupplierId;
                existingProduct.InstallmentTag = product.InstallmentTag;
                existingProduct.Gift = product.Gift;
                existingProduct.Description = product.Description;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thông tin sản phẩm thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi cập nhật: " + ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id = product.Id });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Create()
        //{

        //}
    }
}
