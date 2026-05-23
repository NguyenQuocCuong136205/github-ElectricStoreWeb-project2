using ElectricStore_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.Suppliers
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ElectronicStoreContext _context;

        public SupplierRepository(ElectronicStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return await _context.Suppliers
                .Include(s => s.Products)
                .Include(s => s.StockLogs)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier?> GetSupplierByNameAsync(string name)
        {
            return await _context.Suppliers.SingleOrDefaultAsync(s => s.Name == name);
        }

        public async Task<bool> CreateSupplierAsync(Supplier supplier)
        {
            try
            {
                await _context.Suppliers.AddAsync(supplier);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditSupplierAsync(Supplier supplier)
        {
            try
            {
                var existing = await _context.Suppliers.FindAsync(supplier.Id);
                if (existing == null) return false;
                
                existing.Name = supplier.Name;
                existing.Address = supplier.Address;
                existing.Phone = supplier.Phone;
                existing.Email = supplier.Email;
                
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null) return false;
                _context.Suppliers.Remove(supplier);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
