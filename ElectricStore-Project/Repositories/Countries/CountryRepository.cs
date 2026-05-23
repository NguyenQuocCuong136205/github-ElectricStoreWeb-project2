using ElectricStore_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.Countries
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ElectronicStoreContext _context;

        public CountryRepository(ElectronicStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            return await _context.Countries.Include(c => c.Products).SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Country?> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.SingleOrDefaultAsync(c => c.Country1 == name);
        }

        public async Task<bool> CreateCountryAsync(Country country)
        {
            try
            {
                await _context.Countries.AddAsync(country);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditCountryAsync(Country country)
        {
            try
            {
                var existing = await _context.Countries.FindAsync(country.Id);
                if (existing == null) return false;
                existing.Country1 = country.Country1;
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            try
            {
                var country = await _context.Countries.FindAsync(id);
                if (country == null) return false;
                _context.Countries.Remove(country);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
