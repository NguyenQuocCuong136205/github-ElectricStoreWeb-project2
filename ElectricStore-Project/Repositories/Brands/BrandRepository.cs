using ElectricStore_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.Brands
{
    public class BrandRepository : IBrandRepository
    {
        ElectronicStoreContext _context;
        public BrandRepository(ElectronicStoreContext electronicStoreContext)
        {
            this._context = electronicStoreContext;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            var allBrands = await _context.Brands.ToListAsync();
            return allBrands;
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            var brand = await _context.Brands.Include(b => b.Products).SingleOrDefaultAsync(b => b.Id == id);
            return brand;
        }

        public async Task<Brand?> GetBrandByNameAsync(string name)
        {
            var brand = await _context.Brands.SingleOrDefaultAsync(b => b.BrandName == name);
            return brand;
        }

        public async Task<bool> CreateNewBrand(Brand brand)
        {
            try
            {
                var addedBrand = await _context.Brands.AddAsync(brand);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditBrand(Brand brand)
        {
            try
            {
                var existingBrand = await _context.Brands.FindAsync(brand.Id);
                if (existingBrand == null)
                {
                    return false;
                }
                existingBrand.BrandName = brand.BrandName;
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBrand(int id)
        {
            try
            {
                var brandToDelete = await _context.Brands.FindAsync(id);
                if (brandToDelete == null)
                {
                    return false;
                }
                _context.Brands.Remove(brandToDelete);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
