namespace ElectricStore_Project.DTOs.Login
{
    public class LoginResultDTO
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
