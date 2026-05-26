using ElectricStore_Project.DTOs.Addresses;
using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.Address
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressDTO>> GetAllAddressOfaUserAysnc(int id);
        Task<IEnumerable<ElectricStore_Project.Models.Address>> GetAllAddressesAsync();

        Task<IEnumerable<ElectricStore_Project.Models.Address>> GetAllAddressOfaUSerAsync(int userID);

        Task<bool> CreateNewAddressAsync(ElectricStore_Project.Models.Address newAddress);

        Task<bool> DeleteAddressAsync(int id);

        Task<bool> UpdateAddressAsync(ElectricStore_Project.Models.Address updatedAddress);
    }
}
