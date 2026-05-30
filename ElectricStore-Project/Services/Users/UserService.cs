using ElectricStore_Project.DTOs.Addresses;
using ElectricStore_Project.DTOs.Customers;
using ElectricStore_Project.DTOs.Login;
using ElectricStore_Project.DTOs.Registers;
using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.Address;
using ElectricStore_Project.Repositories.Customers;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ElectricStore_Project.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IDistributedCache _cache;
        public UserService(ICustomerRepository customerRepository, IDistributedCache cache, IAddressRepository addressRepository)
        {
            this._customerRepository = customerRepository;
            this._addressRepository = addressRepository;
            this._cache = cache;
        }
        public async Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            var user = await _customerRepository.GetUserByEmailForLoginAsync(loginRequest.Email);

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
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role == 1 ? "Admin" : "User"
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

        public async Task<ElectricStore_Project.Models.User?> GetUserByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<ShowableCustomerInfor?> GetUserProfileAsync(int userId)
        {
            string cacheKey = $"user_profile_{userId}";
            var cachedData = await _cache.GetStringAsync(cacheKey); // 1. Thử lấy dữ liệu từ Redis

            if (!string.IsNullOrEmpty(cachedData))
            {
                Console.WriteLine($"===> [CACHE HIT] Lấy dữ liệu thành công từ Memory Cache cho key: '{cacheKey}'");
                return JsonSerializer.Deserialize<ShowableCustomerInfor>(cachedData); // Trả về dữ liệu từ Cache ngay lập tức
            }

            Console.WriteLine($"===> [CACHE MISS] Không tìm thấy cache. Tiến hành truy vấn SQL Server cho key: '{cacheKey}'"); // 2. Cache Miss -> Ghi log truy vấn Database

            var user = await _customerRepository.GetCustomerByIdAsync(userId); // 2. Cache Miss -> Lấy từ Database (Nhớ nạp thêm bảng Address liên quan)
            var addresses = await _addressRepository.GetAllAddressOfaUSerAsync(userId);

            if (user == null) return null;

            var profileDto = new ShowableCustomerInfor  // Chuyển đổi sang DTO để loại bỏ dữ liệu thừa và tránh vòng lặp tham chiếu
            {
                Id = user.Id,
                UserName = user.FullName,
                UserEmail = user.Email,
                UserPhoneNumber = user.Phone,
                Address = addresses.Select(a => new AddressDTO { Id = a.Id, Address = a.FullAddress }).ToList()
                //Address = addresses.Select(a => new AddressDTO { a.Id, a.FullAddress })
                // mock data
                //Address = new List<string>
                //{
                //    "123 Đường ABC, Phường XYZ, Quận 1, TP.HCM",
                //    "456 Đường DEF, Phường UVW, Quận 2, TP.HCM"
                //}
            };

            var cacheOptions = new DistributedCacheEntryOptions // 3. Lưu lại vào Redis với thời hạn hết hạn (TTL), ví dụ là 10 phút
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            var serializedData = JsonSerializer.Serialize(profileDto);
            await _cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
            return profileDto;
        }

        public async Task ClearUserProfileCacheAsync(int userId)
        {
            string cacheKey = $"user_profile_{userId}";
            await _cache.RemoveAsync(cacheKey);
            Console.WriteLine($"===> [CACHE INVALIDATED] Đã xóa cache thành công cho key: '{cacheKey}'");
        }
    }
}
