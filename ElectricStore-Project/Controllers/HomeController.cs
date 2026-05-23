using ElectricStore_Project.Models;
using ElectricStore_Project.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElectricStore_Project.Controllers
{
    public class HomeController : Controller
    {
        IProductService productService;
        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await productService.GetProductDisplayDTOsAsync();
            return View(allProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        // sử dụng để vứt các mã lỗi và thông tin lỗi vào trang lỗi, giúp người dùng hiểu rõ hơn về lỗi đã xảy ra và có thể cung cấp thông tin hữu ích cho việc gỡ lỗi.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
