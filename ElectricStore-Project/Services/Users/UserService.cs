using ElectricStore_Project.DTOs.Login;
using ElectricStore_Project.DTOs.Registers;
using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.Customers;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ElectricStore_Project.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ICustomerRepository _customerRepository;
        public UserService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public async Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            var user = await _customerRepository.GetCustomerByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                return new LoginResultDTO
                {
                    Success = false,
                    ErrorMessage = "Tài khoản không tồn tại."
                };
            }

            if (user.IsActive == false)
            {
                return new LoginResultDTO
                {
                    Success = false,
                    ErrorMessage = "Tài khoản hiện tại đang bị khóa"
                };
            }

            if (!VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                return new LoginResultDTO
                {
                    Success = false,
                    ErrorMessage = "Tài khoản hoặc mật khẩu không chính xác."
                };
            }

            return new LoginResultDTO
            {
                Success = true,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.RoleId == 1 ? "Admin" : "User"
            };
        }

        private bool VerifyPassword(string inputPassword, string? storedPasswordHash)
        {
            if (string.IsNullOrEmpty(storedPasswordHash))
            {
                return false;
            }

            // 1. Kiểm tra đối chiếu mật khẩu dạng thô (dữ liệu mẫu cũ)
            if (inputPassword == storedPasswordHash)
            {
                return true;
            }

            // 2. Kiểm tra đối chiếu mật khẩu dạng băm SHA256 (tài khoản đăng ký mới)
            if (HashPassword(inputPassword) == storedPasswordHash)
            {
                return true;
            }

            return false;
        }

        public async Task<RegisterResultDTO> RegisterAsync(RegisterRequestDTO registerRequest)
        {
            var email = await _customerRepository.GetCustomerByEmailAsync(registerRequest.Email ?? "");
            var phone = await _customerRepository.GetCustomerByPhoneAsync(registerRequest.PhoneNumber ?? "");

            if (email != null)
            {
                return new RegisterResultDTO
                {
                    Success = false,
                    ErrorMessage = "Email đã được sử dụng!"
                };
            }

            if (phone != null)
            {
                return new RegisterResultDTO
                {
                    Success = false,
                    ErrorMessage = "Số điện thoại đã được sử dụng!"
                };
            }

            if (!IsSecurePassword(registerRequest.Password))
            {
                return new RegisterResultDTO
                {
                    Success = false,
                    ErrorMessage = "Mật khẩu không đạt yêu cầu độ an toàn!"
                };
            }

            // Tiến hành băm mật khẩu bằng SHA256 trước khi lưu xuống cơ sở dữ liệu
            string hashedPassword = HashPassword(registerRequest.Password ?? "");
            
            // 4. Khởi tạo đối tượng User mới để lưu
            var newUser = new User
            {
                RoleId = 2, // Mặc định là User thường
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                Phone = registerRequest.PhoneNumber,
                PasswordHash = hashedPassword, // Lưu mật khẩu đã mã hóa bảo mật
                CreateAt = DateTime.Now,
                IsActive = true
            };

            bool isSaved = await _customerRepository.CreateCustomerAsync(newUser);
            if (!isSaved)
            {
                return new RegisterResultDTO { Success = false, ErrorMessage = "Có lỗi xảy ra trong quá trình lưu dữ liệu. Vui lòng thử lại!" };
            }
            return new RegisterResultDTO { Success = true };
        }

        private bool IsSecurePassword(string? password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            // Regex quy định: Độ dài >= 10, có ít nhất 1 chữ hoa, 1 chữ thường, 1 số, 1 ký tự đặc biệt
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{10,}$";

            return Regex.IsMatch(password, pattern);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _customerRepository.GetCustomerByEmailAsync(email);
        }

        private string HashPassword(string? password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}