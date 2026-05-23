using ElectricStore_Project.Models;
using ElectricStore_Project.Services.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace ElectricStore_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Name))
            {
                TempData["ErrorMessage"] = "Tên nhà cung cấp không được bỏ trống.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _supplierService.CreateSupplierAsync(supplier);
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
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy nhà cung cấp.";
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Name))
            {
                TempData["ErrorMessage"] = "Tên nhà cung cấp không được bỏ trống.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _supplierService.EditSupplierAsync(supplier);
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
            var result = await _supplierService.DeleteSupplierAsync(id);
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
