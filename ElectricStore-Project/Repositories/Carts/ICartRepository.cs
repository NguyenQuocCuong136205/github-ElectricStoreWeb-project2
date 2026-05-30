using System.Threading.Tasks;
using ElectricStore_Project.Models;

namespace ElectricStore_Project.Repositories.Carts
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByIdAsync(int cartId);
        
        Task<Cart?> GetCartByUserIdAsync(int userId);
        
        Task CreateCartAsync(Cart cart);
        
        Task AddCartItemAsync(CartItem cartItem);
        
        Task UpdateCartItemAsync(CartItem cartItem);
        
        Task DeleteCartItemAsync(int cartItemId);
        
        Task<CartItem?> GetCartItemByIdAsync(int cartItemId);
    }
}
