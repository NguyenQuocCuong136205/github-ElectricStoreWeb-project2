using ElectricStore_Project.DTOs.Customers;
using ElectricStore_Project.DTOs.Login;
using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.Customers
{
    public interface ICustomerRepository
    {
        Task <UserLoginDataDTO?> GetUserByEmailForLoginAsync(string email);
        Task<IEnumerable<ShowableCustomerInfor>> GetAllCustomersAsync();
        //Task<ShowableCustomerInfor> GetAllInforOfaCustomerAsync(int id);
        Task<User?> GetCustomerByIdAsync(int id); // just select in user table, not include address, order, ... 
        Task<User?> GetCustomerByEmailAsync(string email);
        Task<User?> GetCustomerByPhoneAsync(string phone);
        Task<bool> CreateCustomerAsync(User customer);
        Task<bool> EditCustomerAsync(int id);
        Task<bool> DeteleCustomerAsymc(int id);
    }
}
