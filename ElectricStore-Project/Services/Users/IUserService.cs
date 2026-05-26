using ElectricStore_Project.DTOs.Login;
using ElectricStore_Project.DTOs.Registers;

namespace ElectricStore_Project.Services.Users
{
    public interface IUserService
    {
        Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest);

        Task<RegisterResultDTO> RegisterAsync(RegisterRequestDTO registerRequest);
        Task<ElectricStore_Project.Models.User?> GetUserByIdAsync(int id);
        Task<ElectricStore_Project.Models.User?> GetUserByEmailAsync(string email);
        Task<ElectricStore_Project.DTOs.Customers.ShowableCustomerInfor?> GetUserProfileAsync(int userId); // dùng hàm này để lấy cả địa chỉ của user
        Task ClearUserProfileCacheAsync(int userId);
    }
}
