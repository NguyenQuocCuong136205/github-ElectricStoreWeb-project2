    using ElectricStore_Project.Models;
using ElectricStore_Project.Services.Brands;
using ElectricStore_Project.Services.Categories;
using ElectricStore_Project.Services.Countries;
using ElectricStore_Project.Services.Products;
using ElectricStore_Project.Services.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly ICountryService countryService;
        private readonly ISupplierService supplierService;
        private readonly ElectronicStoreContext _context;

        public ProductController(IProductService productService, ElectronicStoreContext context, IBrandService brandService, ICategoryService categoryService, ICountryService countryService, ISupplierService supplierService)
        {
            this.productService = productService;
            this._context = context;
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.countryService = countryService;
            this.supplierService = supplierService;
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
            ViewBag.Categories = (await categoryService.GetAllCategoriesAsync()).ToList();
            ViewBag.Brands = (await brandService.GetAllBrandsAsync()).ToList();
            ViewBag.Countries = (await countryService.GetAllCountriesAsync()).ToList();
            ViewBag.Suppliers = (await supplierService.GetAllSuppliersAsync()).ToList();

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
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = (await categoryService.GetAllCategoriesAsync()).ToList();
            ViewBag.Brands = (await brandService.GetAllBrandsAsync()).ToList();
            ViewBag.Countries = (await countryService.GetAllCountriesAsync()).ToList();
            ViewBag.Suppliers = (await supplierService.GetAllSuppliersAsync()).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ElectricStore_Project.DTOs.Products.AllProductInforDTO model, int? MadeIn)
        {
            if (model == null)
            {
                TempData["ErrorMessage"] = "Dữ liệu tạo mới không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            // Load ViewBag data in case we need to return the view on model validation failure
            ViewBag.Categories = (await categoryService.GetAllCategoriesAsync()).ToList();
            ViewBag.Brands = (await brandService.GetAllBrandsAsync()).ToList();
            ViewBag.Countries = (await countryService.GetAllCountriesAsync()).ToList();
            ViewBag.Suppliers = (await supplierService.GetAllSuppliersAsync()).ToList();

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ thông tin bắt buộc.";
                return View(model);
            }

            try
            {
                // 1. Resolve IDs for Category, Brand, and Supplier
                int? categoryId = null;
                if (int.TryParse(model.Category, out int catId))
                {
                    categoryId = catId;
                }
                else if (!string.IsNullOrEmpty(model.Category))
                {
                    var cat = await _context.Categories.FirstOrDefaultAsync(c => c.Name == model.Category);
                    if (cat != null) categoryId = cat.Id;
                }

                int? brandId = null;
                if (int.TryParse(model.Brand, out int bId))
                {
                    brandId = bId;
                }
                else if (!string.IsNullOrEmpty(model.Brand))
                {
                    var brand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandName == model.Brand);
                    if (brand != null) brandId = brand.Id;
                }

                int? supplierId = null;
                if (int.TryParse(model.SupplierName, out int sId))
                {
                    supplierId = sId;
                }
                else if (!string.IsNullOrEmpty(model.SupplierName))
                {
                    var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Name == model.SupplierName);
                    if (supplier != null) supplierId = supplier.Id;
                }

                // 2. Create the Product entity
                var product = new Product
                {
                    Name = model.Name,
                    CategoryId = categoryId,
                    BrandId = brandId,
                    ImportPrice = model.ImportPrice,
                    SalePrice = model.SalePrice,
                    OriginalPrice = model.OriginalPrice,
                    MadeIn = MadeIn,
                    StockQuantity = model.StockQuantity,
                    IsActive = model.IsActive,
                    SupplierId = supplierId,
                    InstallmentTag = model.IstallmentTag,
                    Gift = model.GiftInfo,
                    Description = model.Description,
                    Rating = 5.0m, // Default initial rating
                    SaleCount = 0  // Initial sale count
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync(); // Saves product to generate its Id

                // 3. Add Uploaded Images
                if (model.ImageFiles != null && model.ImageFiles.Any())
                {
                    int order = 1;
                    foreach (var file in model.ImageFiles)
                    {
                        var savedFileName = await SaveUploadedFileAsync(file);
                        if (!string.IsNullOrEmpty(savedFileName))
                        {
                            var img = new ProductImage
                            {
                                ProductId = product.Id,
                                Img = savedFileName,
                                DisplayOrder = order++
                            };
                            _context.ProductImages.Add(img);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                // 4. Add Product Specifications
                if (!string.IsNullOrWhiteSpace(model.CPU))
                {
                    await AddProductSpecification(product.Id, categoryId, "CPU", model.CPU.Trim());
                }
                if (model.Ram > 0)
                {
                    await AddProductSpecification(product.Id, categoryId, "RAM", $"{model.Ram} GB");
                }
                if (model.Memory > 0)
                {
                    await AddProductSpecification(product.Id, categoryId, "Memory", $"{model.Memory} GB");
                }
                if (model.ScreenSize > 0)
                {
                    await AddProductSpecification(product.Id, categoryId, "ScreenSize", $"{model.ScreenSize} inch");
                }
                if (model.PinCapacity > 0)
                {
                    await AddProductSpecification(product.Id, categoryId, "PinCapacity", $"{model.PinCapacity} mAh");
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thêm mới sản phẩm thành công!";
                return RedirectToAction(nameof(Details), new { id = product.Id });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tạo sản phẩm: " + ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBatch([FromForm] List<ElectricStore_Project.DTOs.Products.AllProductInforDTO> products)
        {
            if (products == null || !products.Any())
            {
                return Json(new { success = false, message = "Không có sản phẩm nào trong danh sách." });
            }

            try
            {
                foreach (var model in products)
                {
                    // 1. Resolve IDs for Category, Brand, and Supplier
                    int? categoryId = null;
                    if (int.TryParse(model.Category, out int catId))
                    {
                        categoryId = catId;
                    }
                    else if (!string.IsNullOrEmpty(model.Category))
                    {
                        var cat = await _context.Categories.FirstOrDefaultAsync(c => c.Name == model.Category);
                        if (cat != null) categoryId = cat.Id;
                    }

                    int? brandId = null;
                    if (int.TryParse(model.Brand, out int bId))
                    {
                        brandId = bId;
                    }
                    else if (!string.IsNullOrEmpty(model.Brand))
                    {
                        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandName == model.Brand);
                        if (brand != null) brandId = brand.Id;
                    }

                    int? supplierId = null;
                    if (int.TryParse(model.SupplierName, out int sId))
                    {
                        supplierId = sId;
                    }
                    else if (!string.IsNullOrEmpty(model.SupplierName))
                    {
                        var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Name == model.SupplierName);
                        if (supplier != null) supplierId = supplier.Id;
                    }

                    // 2. Create the Product entity
                    var product = new Product
                    {
                        Name = model.Name,
                        CategoryId = categoryId,
                        BrandId = brandId,
                        ImportPrice = model.ImportPrice,
                        SalePrice = model.SalePrice,
                        OriginalPrice = model.OriginalPrice,
                        MadeIn = model.MadeIn,
                        StockQuantity = model.StockQuantity,
                        IsActive = model.IsActive,
                        SupplierId = supplierId,
                        InstallmentTag = model.IstallmentTag,
                        Gift = model.GiftInfo,
                        Description = model.Description,
                        Rating = 5.0m,
                        SaleCount = 0
                    };

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    // 3. Add Uploaded Images
                    if (model.ImageFiles != null && model.ImageFiles.Any())
                    {
                        int order = 1;
                        foreach (var file in model.ImageFiles)
                        {
                            var savedFileName = await SaveUploadedFileAsync(file);
                            if (!string.IsNullOrEmpty(savedFileName))
                            {
                                var img = new ProductImage
                                {
                                    ProductId = product.Id,
                                    Img = savedFileName,
                                    DisplayOrder = order++
                                };
                                _context.ProductImages.Add(img);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }

                    // 4. Add Product Specifications
                    if (!string.IsNullOrWhiteSpace(model.CPU))
                    {
                        await AddProductSpecification(product.Id, categoryId, "CPU", model.CPU.Trim());
                    }
                    if (model.Ram > 0)
                    {
                        await AddProductSpecification(product.Id, categoryId, "RAM", $"{model.Ram} GB");
                    }
                    if (model.Memory > 0)
                    {
                        await AddProductSpecification(product.Id, categoryId, "Memory", $"{model.Memory} GB");
                    }
                    if (model.ScreenSize > 0)
                    {
                        await AddProductSpecification(product.Id, categoryId, "ScreenSize", $"{model.ScreenSize} inch");
                    }
                    if (model.PinCapacity > 0)
                    {
                        await AddProductSpecification(product.Id, categoryId, "PinCapacity", $"{model.PinCapacity} mAh");
                    }
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, redirectUrl = Url.Action(nameof(Index)) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi tạo sản phẩm: " + ex.Message });
            }
        }

        private async Task<string?> SaveUploadedFileAsync(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        private async Task AddProductSpecification(int productId, int? categoryId, string attributeName, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            var attribute = await _context.SpecificationAttributes
                .FirstOrDefaultAsync(a => a.Name == attributeName && (categoryId == null || a.CategoryId == categoryId));

            if (attribute == null)
            {
                attribute = new SpecificationAttribute
                {
                    Name = attributeName,
                    CategoryId = categoryId
                };
                _context.SpecificationAttributes.Add(attribute);
                await _context.SaveChangesAsync();
            }

            var mapping = new ProductSpecificationMapping
            {
                ProductId = productId,
                AttributeId = attribute.Id,
                Value = value
            };
            _context.ProductSpecificationMappings.Add(mapping);
        }
    }
}
