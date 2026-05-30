using System.Collections.Generic;
using System.Linq;

namespace ElectricStore_Project.DTOs.Carts
{
    public class CartDisplayDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemDisplayDTO> Items { get; set; } = new List<CartItemDisplayDTO>();
        
        // Total price of the entire cart
        public decimal TotalCartPrice => Items.Sum(item => item.SubTotal);
    }
}
