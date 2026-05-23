using ElectricStore_Project.DTOs.Login;
using ElectricStore_Project.DTOs.Registers;

namespace ElectricStore_Project.Services.Users
{
    public interface IUserService
    {
        Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest);

        Task<RegisterResultDTO> RegisterAsync(RegisterRequestDTO registerRequest);

        Task<ElectricStore_Project.Models.User?> GetUserByEmailAsync(string email);
    }
}
