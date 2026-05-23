using ElectricStore_Project.DTOs.Suppliers;
using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.Suppliers;

namespace ElectricStore_Project.Services.Suppliers
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierRepository.GetAllSuppliersAsync();
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return await _supplierRepository.GetSupplierByIdAsync(id);
        }

        public async Task<SupplierResultDTO> CreateSupplierAsync(Supplier supplier)
        {
            var existing = await _supplierRepository.GetSupplierByNameAsync(supplier.Name);
            if (existing != null)
            {
                return new SupplierResultDTO
                {
                    IsSuccess = false,
                    Message = "Tên nhà cung cấp này đã tồn tại."
                };
            }

            if (await _supplierRepository.CreateSupplierAsync(supplier))
            {
                return new SupplierResultDTO
                {
                    IsSuccess = true,
                    Message = "Tạo nhà cung cấp thành công."
                };
            }

            return new SupplierResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình tạo nhà cung cấp."
            };
        }

        public async Task<SupplierResultDTO> EditSupplierAsync(Supplier supplier)
        {
            var existing = await _supplierRepository.GetSupplierByIdAsync(supplier.Id);
            if (existing == null)
            {
                return new SupplierResultDTO
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy nhà cung cấp cần chỉnh sửa."
                };
            }

            var duplicate = await _supplierRepository.GetSupplierByNameAsync(supplier.Name);
            if (duplicate != null && duplicate.Id != supplier.Id)
            {
                return new SupplierResultDTO
                {
                    IsSuccess = false,
                    Message = "Tên nhà cung cấp này đã được sử dụng bởi nhà cung cấp khác."
                };
            }

            if (await _supplierRepository.EditSupplierAsync(supplier))
            {
                return new SupplierResultDTO
                {
                    IsSuccess = true,
                    Message = "Cập nhật nhà cung cấp thành công."
                };
            }

            return new SupplierResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình cập nhật nhà cung cấp."
            };
        }

        public async Task<SupplierResultDTO> DeleteSupplierAsync(int id)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return new SupplierResultDTO
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy nhà cung cấp cần xóa."
                };
            }

            if (supplier.Products != null && supplier.Products.Any())
            {
                return new SupplierResultDTO
                {
                    IsSuccess = false,
                    Message = "Không thể xóa nhà cung cấp này vì có sản phẩm đang thuộc nhà cung cấp này."
                };
            }

            if (supplier.StockLogs != null && supplier.StockLogs.Any())
            {
                return new SupplierResultDTO
                {
                    IsSuccess = false,
                    Message = "Không thể xóa nhà cung cấp vì đã có lịch sử nhập hàng liên kết."
                };
            }

            if (await _supplierRepository.DeleteSupplierAsync(id))
            {
                return new SupplierResultDTO
                {
                    IsSuccess = true,
                    Message = "Xóa nhà cung cấp thành công."
                };
            }

            return new SupplierResultDTO
            {
                IsSuccess = false,
                Message = "Gặp lỗi trong quá trình xóa nhà cung cấp."
            };
        }
    }
}
