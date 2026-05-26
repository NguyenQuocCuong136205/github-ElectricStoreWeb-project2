namespace ElectricStore_Project.Services.Addresses
{
    public interface IAddressService
    {
        Task<IEnumerable<ElectricStore_Project.Models.Address>> GetAllAddressesAsync();

        Task<IEnumerable<ElectricStore_Project.Models.Address>> GetAllAddressOfaUSerAsync(int userID);

        Task<bool> CreateNewAddressAsync(ElectricStore_Project.Models.Address newAddress);

        Task<bool> DeleteAddressAsync(int id);

        Task<bool> UpdateAddressAsync(ElectricStore_Project.Models.Address updatedAddress);
    }
}
