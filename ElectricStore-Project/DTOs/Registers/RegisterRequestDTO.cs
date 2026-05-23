using System.ComponentModel.DataAnnotations;
namespace ElectricStore_Project.DTOs.Registers
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Tên không được để trống!")]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ!")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{10,}$", 
            ErrorMessage = "Mật khẩu tối thiểu 10 ký tự, gồm ít nhất 1 chữ hoa, 1 chữ thường, 1 chữ số và 1 ký tự đặc biệt!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không trùng khớp!")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}