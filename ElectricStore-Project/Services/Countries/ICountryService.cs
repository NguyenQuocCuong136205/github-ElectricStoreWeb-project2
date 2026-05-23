using ElectricStore_Project.DTOs.Countries;
using ElectricStore_Project.Models;

namespace ElectricStore_Project.Services.Countries
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country?> GetCountryByIdAsync(int id);
        Task<CountryResultDTO> CreateCountryAsync(Country country);
        Task<CountryResultDTO> EditCountryAsync(Country country);
        Task<CountryResultDTO> DeleteCountryAsync(int id);
    }
}
