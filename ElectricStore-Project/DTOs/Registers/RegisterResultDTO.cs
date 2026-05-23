namespace ElectricStore_Project.DTOs.Registers
{
    public class RegisterResultDTO
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
    }
    }
