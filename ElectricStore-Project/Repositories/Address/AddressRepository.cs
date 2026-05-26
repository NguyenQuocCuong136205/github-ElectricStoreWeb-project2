using ElectricStore_Project.Models;
using ElectricStore_Project.DTOs.Addresses;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.Address
{
    public class AddressRepository : IAddressRepository
    {
        ElectronicStoreContext _context;

        public AddressRepository(ElectronicStoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AddressDTO>> GetAllAddressOfaUserAysnc(int id)
        {
            return await _context.Addresses.Where(a => a.UserId == id)
                .Select(a => new AddressDTO
                {
                    Id = a.Id,
                    Address = a.FullAddress
                }).ToListAsync();
        }
        public async Task<IEnumerable<ElectricStore_Project.Models.Address>> GetAllAddressesAsync()
        {
            var allAddresses = await _context.Addresses.ToListAsync();
            return allAddresses;
        }

        public async Task<IEnumerable<ElectricStore_Project.Models.Address>> GetAllAddressOfaUSerAsync(int userID)
        {
            var allAddressesOfaUser = await _context.Addresses.Where(a => a.UserId == userID).ToListAsync();
            return allAddressesOfaUser;
        }

        public async Task<bool> CreateNewAddressAsync(ElectricStore_Project.Models.Address newAddress)
        {
            try
            {
                var addAddress = _context.Addresses.AddAsync(newAddress);
                var saveChanges = await _context.SaveChangesAsync();
                return saveChanges > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAddressAsync(int id)
        {
            try
            {
                var addressToDelete = await _context.Addresses.FindAsync(id);
                if (addressToDelete == null)
                {
                    return false;
                }
                _context.Addresses.Remove(addressToDelete);
                var saveChanges = await _context.SaveChangesAsync();
                return saveChanges > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAddressAsync(ElectricStore_Project.Models.Address updatedAddress)
        {
            try
            {
                var existingAddress = await _context.Addresses.FindAsync(updatedAddress.Id);
                if (existingAddress == null)
                {
                    return false;
                }

                existingAddress.FullAddress = updatedAddress.FullAddress;
                _context.Addresses.Update(existingAddress);
                var saveChanges = await _context.SaveChangesAsync();
                return saveChanges > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
