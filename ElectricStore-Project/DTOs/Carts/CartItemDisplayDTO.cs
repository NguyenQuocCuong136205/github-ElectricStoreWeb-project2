using System.Collections.Generic;

namespace ElectricStore_Project.DTOs.Carts
{
    public class CartItemDisplayDTO
    {
        public int Id { get; set; } // CartItemId
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SubTotal => Quantity * SalePrice;
        
        // Dictionary containing Specification Attribute Name -> Value (e.g. "RAM" -> "8GB")
        public Dictionary<string, string> Specifications { get; set; } = new Dictionary<string, string>();
    }
}
