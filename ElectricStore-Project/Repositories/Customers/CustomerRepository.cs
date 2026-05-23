using ElectricStore_Project.DTOs.Customers;
using ElectricStore_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricStore_Project.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ElectronicStoreContext _context;

        public CustomerRepository(ElectronicStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShowableCustomerInfor>> GetAllCustomersAsync()
        {
            var customers = await _context.Users.Where(u => u.RoleId == 2).ToListAsync();

            var customerList = customers.Select(c => new ShowableCustomerInfor
            {
                UserName = c.FullName,
                UserEmail = c.Email,
                UserPhoneNumber = c.Phone,
                CreatedDate = c.CreateAt
            });

            return customerList;
        }

        public async Task<User?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetCustomerByPhoneAsync(string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone && u.RoleId == 2);
        }

        public async Task<bool> CreateCustomerAsync(User customer)
        {
            try
            {
                await _context.Users.AddAsync(customer);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditCustomerAsync(int id)
        {
            try
            {
                var existingCustomer = await _context.Users.FindAsync(id);
                if (existingCustomer == null)
                {
                    return false;
                }

                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeteleCustomerAsymc(int id)
        {
            try
            {
                var customer = await _context.Users.FindAsync(id);
                if (customer == null)
                {
                    return false;
                }
                _context.Users.Remove(customer);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
