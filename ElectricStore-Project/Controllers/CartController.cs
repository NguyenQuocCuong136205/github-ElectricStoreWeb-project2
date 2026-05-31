using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectricStore_Project.Services.Carts;

namespace ElectricStore_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để xem giỏ hàng!";
                return RedirectToAction("Login", "User");
            }

            int userId = int.Parse(userIdStr);
            var cartDto = await _cartService.GetCartDisplayByUserIdAsync(userId);
            
            return View(cartDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để thực hiện hành động này!";
                return RedirectToAction("Login", "User");
            }

            try
            {
                await _cartService.AddToCartAsync(int.Parse(userIdStr), productId, quantity);
                TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng thành công!";
            }
            catch (System.Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            
            // Redirect back to referring page or Cart index
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "User");
            }

            await _cartService.UpdateCartItemQuantityAsync(cartItemId, quantity);
            TempData["SuccessMessage"] = "Cập nhật số lượng thành công!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "User");
            }

            await _cartService.RemoveCartItemAsync(cartItemId);
            TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng!";
            return RedirectToAction("Index");
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
