using System.Threading.Tasks;
using ElectricStore_Project.DTOs.Carts;

namespace ElectricStore_Project.Services.Carts
{
    public interface ICartService
    {
        Task<CartDisplayDTO?> GetCartDisplayByUserIdAsync(int userId);
        
        Task<CartDisplayDTO?> GetCartDisplayByIdAsync(int cartId);
        
        Task AddToCartAsync(int userId, int productId, int quantity);
        
        Task UpdateCartItemQuantityAsync(int cartItemId, int newQuantity);
        
        Task RemoveCartItemAsync(int cartItemId);
    }
}
