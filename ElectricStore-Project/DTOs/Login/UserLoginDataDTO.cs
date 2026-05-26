namespace ElectricStore_Project.DTOs.Login
{
    public class UserLoginDataDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public int? Role { get; set; }
        public bool? IsActive { get; set; }
    }
}
