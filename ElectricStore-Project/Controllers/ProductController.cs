using Microsoft.AspNetCore.Mvc;

namespace ElectricStore_Project.Controllers
{
    public class ProductController : Controller
    {

        public async Task<IActionResult> Search(string? keyword)
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
