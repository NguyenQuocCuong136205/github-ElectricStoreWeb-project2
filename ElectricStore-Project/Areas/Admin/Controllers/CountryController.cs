using ElectricStore_Project.Models;
using ElectricStore_Project.Services.Countries;
using Microsoft.AspNetCore.Mvc;

namespace ElectricStore_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (string.IsNullOrWhiteSpace(country.Country1))
            {
                TempData["ErrorMessage"] = "Tên quốc gia không được bỏ trống.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _countryService.CreateCountryAsync(country);
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
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy quốc gia.";
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            if (string.IsNullOrWhiteSpace(country.Country1))
            {
                TempData["ErrorMessage"] = "Tên quốc gia không được bỏ trống.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _countryService.EditCountryAsync(country);
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
            var result = await _countryService.DeleteCountryAsync(id);
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
