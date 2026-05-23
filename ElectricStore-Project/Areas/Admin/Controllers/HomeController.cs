using Microsoft.AspNetCore.Mvc;
using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories;
using ElectricStore_Project.Repositories.Products;
using ElectricStore_Project.Services.Products;

namespace ElectricStore_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        IProductService productService;
        public HomeController(IProductService productService) { 
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> ShowProductById()
        {
            return View();
        }
    }
}
