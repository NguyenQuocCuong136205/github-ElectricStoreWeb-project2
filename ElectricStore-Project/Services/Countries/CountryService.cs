using ElectricStore_Project.DTOs.Countries;
using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.Countries;

namespace ElectricStore_Project.Services.Countries
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _countryRepository.GetAllCountriesAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            return await _countryRepository.GetCountryByIdAsync(id);
        }

        public async Task<CountryResultDTO> CreateCountryAsync(Country country)
        {
            var existing = await _countryRepository.GetCountryByNameAsync(country.Country1);
            if (existing != null)
            {
                return new CountryResultDTO
                {
                    IsSuccess = false,
                    Message = "Tên quốc gia này đã tồn tại trong hệ thống."
                };
            }

            if (await _countryRepository.CreateCountryAsync(country))
            {
                return new CountryResultDTO
                {
                    IsSuccess = true,
                    Message = "Tạo quốc gia thành công."
                };
            }

            return new CountryResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình tạo quốc gia."
            };
        }

        public async Task<CountryResultDTO> EditCountryAsync(Country country)
        {
            var existing = await _countryRepository.GetCountryByIdAsync(country.Id);
            if (existing == null)
            {
                return new CountryResultDTO
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy quốc gia cần chỉnh sửa."
                };
            }

            var duplicate = await _countryRepository.GetCountryByNameAsync(country.Country1);
            if (duplicate != null && duplicate.Id != country.Id)
            {
                return new CountryResultDTO
                {
                    IsSuccess = false,
                    Message = "Tên quốc gia này đã được sử dụng bởi quốc gia khác."
                };
            }

            if (await _countryRepository.EditCountryAsync(country))
            {
                return new CountryResultDTO
                {
                    IsSuccess = true,
                    Message = "Cập nhật quốc gia thành công."
                };
            }

            return new CountryResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình cập nhật quốc gia."
            };
        }

        public async Task<CountryResultDTO> DeleteCountryAsync(int id)
        {
            var country = await _countryRepository.GetCountryByIdAsync(id);
            if (country == null)
            {
                return new CountryResultDTO
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy quốc gia cần xóa."
                };
            }

            if (country.Products != null && country.Products.Any())
            {
                return new CountryResultDTO
                {
                    IsSuccess = false,
                    Message = "Không thể xóa quốc gia này vì đang có sản phẩm thuộc quốc gia này."
                };
            }

            if (await _countryRepository.DeleteCountryAsync(id))
            {
                return new CountryResultDTO
                {
                    IsSuccess = true,
                    Message = "Xóa quốc gia thành công."
                };
            }

            return new CountryResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình xóa quốc gia."
            };
        }
    }
}
