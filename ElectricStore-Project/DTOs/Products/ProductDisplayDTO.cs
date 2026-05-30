using ElectricStore_Project.Models;

namespace ElectricStore_Project.DTOs.Products
{
    public class ProductDisplayDTO
    {
        // get all infor of product which admin want to show
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public decimal OriginalPrice {  get; set; }
        public decimal SalePrice { get; set; }
        //public string ImageUrl { get; set; }
        //public decimal imporetprice { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierName { get; set; }
        public string MadeIn { get; set; }
        public string SalceCount { get; set; }
        public decimal Rating { get; set; }
        public string GiftInfo { get; set; }
        public string IstallmentTag { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<string> ImageList { get; set; } = new List<string>();
    }
}
