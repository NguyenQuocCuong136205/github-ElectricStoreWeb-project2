using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.Suppliers
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task<Supplier?> GetSupplierByNameAsync(string name);
        Task<bool> CreateSupplierAsync(Supplier supplier);
        Task<bool> EditSupplierAsync(Supplier supplier);
        Task<bool> DeleteSupplierAsync(int id);
    }
}
