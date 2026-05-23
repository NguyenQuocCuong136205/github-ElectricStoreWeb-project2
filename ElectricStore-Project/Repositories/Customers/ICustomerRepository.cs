using ElectricStore_Project.Models;
using ElectricStore_Project.DTOs.Customers;

namespace ElectricStore_Project.Repositories.Customers
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<ShowableCustomerInfor>> GetAllCustomersAsync();

        Task<User?> GetCustomerByEmailAsync(string email);

        Task<User?> GetCustomerByPhoneAsync(string phone);

        Task<bool> CreateCustomerAsync(User customer);

        Task<bool> EditCustomerAsync(int id);

        Task<bool> DeteleCustomerAsymc(int id);
    }
}
