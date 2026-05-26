using ElectricStore_Project.DTOs.Addresses;

namespace ElectricStore_Project.DTOs.Customers
{
    public class ShowableCustomerInfor
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<AddressDTO> Address { get; set; } = new();
    }
}
