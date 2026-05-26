using ElectricStore_Project.Services.Addresses;
using Microsoft.AspNetCore.Mvc;

namespace ElectricStore_Project.Areas.Admin.Controllers
{
    public class AddressController : Controller
    {
        IAddressService addressService;
        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }

}
