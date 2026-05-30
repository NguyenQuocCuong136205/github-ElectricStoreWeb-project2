namespace ElectricStore_Project.DTOs.Products
{
    public class AllProductInforDTO
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierName { get; set; }
        public string GiftInfo { get; set; }
        public string IstallmentTag { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<string> ImageList { get; set; } = new List<string>();
        public int Ram {  get; set; }
        public string CPU { get; set; }
        public int Memory { get; set; }
        public double ScreenSize { get; set; }
        public int PinCapacity { get; set; }

        public int? MadeIn { get; set; }
        public List<Microsoft.AspNetCore.Http.IFormFile>? ImageFiles { get; set; }
    }
}
