using Microsoft.AspNetCore.Mvc;
using ElectricStore_Project.DTOs.Login;
using ElectricStore_Project.DTOs.Registers;
using ElectricStore_Project.Services.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ElectricStore_Project.Models;


namespace ElectricStore_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public ActionResult Login() => View();

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestDTO loginForm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginForm);
            }

            LoginResultDTO result = await _userService.LoginAsync(loginForm);

            // if login successfully -> provider that account a cookie
            if (result.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                    new Claim(ClaimTypes.Name, result.FullName ?? "User"),
                    new Claim(ClaimTypes.Email, result.Email ?? ""),
                    new Claim(ClaimTypes.Role, result.Role ?? "User")
                };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // IsPersistent == true -> permanent, IsPersistent == false-> , session  
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);

                if (result.Role == "Admin")
                {
                    TempData["SuccessMessage"] = "Chào mừng Admin!";
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                TempData["SuccessMessage"] = "Chào mừng quay trở lại!";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Đăng nhập thất bại");
            return View(loginForm);
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register() => View();

        [HttpPost]
        public async Task<ActionResult> Register(RegisterRequestDTO requestForm)
        {
            if (!ModelState.IsValid) return View(requestForm);

            var result = await _userService.RegisterAsync(requestForm);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Đăng ký tài khoản thành công!";
                return RedirectToAction("Login", "User");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Đăng ký thất bại");
            TempData["ErrorMessage"] = result.ErrorMessage;
            return View(requestForm);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Information()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "User");
            }

            var user = await _userService.GetUserProfileAsync(int.Parse(userId));
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View(user);
        }

        public ActionResult PurchasedOrders()
        {
            return View();
        }
    }
}
