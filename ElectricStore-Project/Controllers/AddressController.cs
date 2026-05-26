using ElectricStore_Project.Services.Addresses;
using ElectricStore_Project.Services.Users;
using ElectricStore_Project.DTOs.Addresses;
using Microsoft.AspNetCore.Mvc;

namespace ElectricStore_Project.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;

        public AddressController(IAddressService addressService, IUserService userService)
        {
            _addressService = addressService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult showAllAddresses(int? userID)
        //{
        //    if (userID == null)
        //    {
        //        RedirectToAction("User/Information");
        //    }

        //    var allAddressesOfaUser = _addressService.GetAllAddressOfaUSerAsync((int)userID).Result;

        //    return View(allAddressesOfaUser);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateNewAddress([FromBody] CreateAddressRequestDTO createAddressRequestDTO)
        {
            if (createAddressRequestDTO == null || string.IsNullOrWhiteSpace(createAddressRequestDTO.NewAddress))
            {
                return Json(new { success = false, message = "Địa chỉ không hợp lệ." });
            }

            var newAddress = new Models.Address
            {
                UserId = createAddressRequestDTO.UserID,
                FullAddress = createAddressRequestDTO.NewAddress
            };

            bool result = await _addressService.CreateNewAddressAsync(newAddress);
            if (result)
            {
                // Xóa cache của người dùng để cập nhật địa chỉ mới ngay lập tức trên view
                await _userService.ClearUserProfileCacheAsync(createAddressRequestDTO.UserID);
                return Json(new { success = true, message = "Thêm địa chỉ mới thành công." });
            }

            return Json(new { success = false, message = "Có lỗi xảy ra khi lưu địa chỉ vào hệ thống." });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressRequestDTO updateAddressRequestDTO)
        {
            if (updateAddressRequestDTO == null || string.IsNullOrWhiteSpace(updateAddressRequestDTO.UpdatedAddress))
            {
                return Json(new { success = false, message = "Địa chỉ cập nhật không hợp lệ." });
            }

            var updatedAddress = new Models.Address
            {
                Id = updateAddressRequestDTO.AddressID,
                FullAddress = updateAddressRequestDTO.UpdatedAddress
            };

            bool result = await _addressService.UpdateAddressAsync(updatedAddress);
            if (result)
            {
                // Xóa cache của người dùng để cập nhật ngay lập tức trên view
                await _userService.ClearUserProfileCacheAsync(updateAddressRequestDTO.UserID);
                return Json(new { success = true, message = "Cập nhật địa chỉ thành công." });
            }

            return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật địa chỉ." });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress([FromBody] DeleteAddressRequestDTO deleteAddressRequestDTO)
        {
            if (deleteAddressRequestDTO == null)
            {
                return Json(new { success = false, message = "Yêu cầu không hợp lệ." });
            }

            bool result = await _addressService.DeleteAddressAsync(deleteAddressRequestDTO.AddressID);
            if (result)
            {
                // Xóa cache của người dùng để cập nhật ngay lập tức trên view
                await _userService.ClearUserProfileCacheAsync(deleteAddressRequestDTO.UserID);
                return Json(new { success = true, message = "Xóa địa chỉ thành công." });
            }

            return Json(new { success = false, message = "Có lỗi xảy ra khi xóa địa chỉ." });
        }
    }
}
