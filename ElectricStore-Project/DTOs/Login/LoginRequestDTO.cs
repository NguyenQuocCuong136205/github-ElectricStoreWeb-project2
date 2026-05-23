using System.ComponentModel.DataAnnotations;

namespace ElectricStore_Project.DTOs.Login
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email Không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password không được để trống!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
    }
}
