using ElectricStore_Project.Models;
using ElectricStore_Project.Services.Brands;
using Microsoft.AspNetCore.Mvc;

namespace ElectricStore_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (string.IsNullOrWhiteSpace(brand.BrandName))
            {
                TempData["ErrorMessage"] = "Tên thương hiệu không được bỏ trống.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _brandService.CreateBrandAsync(brand);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy thương hiệu.";
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand brand)
        {
            if (string.IsNullOrWhiteSpace(brand.BrandName))
            {
                TempData["ErrorMessage"] = "Tên thương hiệu không được bỏ trống.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _brandService.EditBrandAsync(brand);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _brandService.DeleteBrandAsync(id);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
