using ElectricStore_Project.DTOs.Suppliers;
using ElectricStore_Project.Models;

namespace ElectricStore_Project.Services.Suppliers
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task<SupplierResultDTO> CreateSupplierAsync(Supplier supplier);
        Task<SupplierResultDTO> EditSupplierAsync(Supplier supplier);
        Task<SupplierResultDTO> DeleteSupplierAsync(int id);
    }
}
