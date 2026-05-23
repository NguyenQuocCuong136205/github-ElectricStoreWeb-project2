using ElectricStore_Project.DTOs.Brands;
using ElectricStore_Project.Models;

namespace ElectricStore_Project.Services.Brands
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();

        Task<Brand?> GetBrandByIdAsync(int id);

        Task<Brand?> GetBrandByNameAsync(string name);

        Task<BrandResultDTO> CreateBrandAsync(Brand brand);

        Task<BrandResultDTO> EditBrandAsync(Brand brand);

        Task<BrandResultDTO> DeleteBrandAsync(int id);
    }
}
