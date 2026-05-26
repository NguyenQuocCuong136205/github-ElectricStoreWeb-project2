using ElectricStore_Project.Repositories.Address;

namespace ElectricStore_Project.Services.Addresses
{
    public class AddressService : IAddressService
    {
        IAddressRepository addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public Task<IEnumerable<ElectricStore_Project.Models.Address>> GetAllAddressesAsync()
        {
            return addressRepository.GetAllAddressesAsync();
        }

        public Task<IEnumerable<ElectricStore_Project.Models.Address>> GetAllAddressOfaUSerAsync(int userID)
        {
            return addressRepository.GetAllAddressOfaUSerAsync(userID);
        }

        public Task<bool> CreateNewAddressAsync(ElectricStore_Project.Models.Address newAddress)
        {
            return addressRepository.CreateNewAddressAsync(newAddress);
        }

        public Task<bool> DeleteAddressAsync(int id)
        {
            return addressRepository.DeleteAddressAsync(id);
        }

        public Task<bool> UpdateAddressAsync(ElectricStore_Project.Models.Address updatedAddress)
        {
            return addressRepository.UpdateAddressAsync(updatedAddress);
        }
    }
}
