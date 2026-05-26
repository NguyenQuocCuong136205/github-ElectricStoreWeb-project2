namespace ElectricStore_Project.DTOs.Addresses
{
    public class UpdateAddressRequestDTO
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string UpdatedAddress { get; set; }
    }
}
