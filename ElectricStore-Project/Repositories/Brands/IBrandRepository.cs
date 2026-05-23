using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.Brands
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();

        Task<Brand?> GetBrandByIdAsync(int id);

        Task<Brand?> GetBrandByNameAsync(string name);

        Task<bool> CreateNewBrand(Brand brand);

        Task<bool> EditBrand(Brand brand); 

        Task<bool> DeleteBrand(int id);
    }
}
