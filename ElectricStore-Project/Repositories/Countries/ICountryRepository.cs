using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.Countries
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country?> GetCountryByIdAsync(int id);
        Task<Country?> GetCountryByNameAsync(string name);
        Task<bool> CreateCountryAsync(Country country);
        Task<bool> EditCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(int id);
    }
}
