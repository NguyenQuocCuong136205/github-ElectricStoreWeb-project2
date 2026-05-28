using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public int? BrandId { get; set; }

    public string? Name { get; set; }
    
    public decimal? SalePrice { get; set; }

    public decimal? ImportPrice { get; set; }

    public int? MadeIn { get; set; }

    public int? StockQuantity { get; set; }

    public decimal? Rating { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public int? SupplierId { get; set; }

    public decimal? OriginalPrice { get; set; }

    public int? SaleCount { get; set; }

    public string? InstallmentTag { get; set; }

    public string? Gift { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<ExportReceiptDetail> ExportReceiptDetails { get; set; } = new List<ExportReceiptDetail>();

    public virtual ICollection<ImportReceiptDetail> ImportReceiptDetails { get; set; } = new List<ImportReceiptDetail>();

    public virtual Country? MadeInNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<StockLog> StockLogs { get; set; } = new List<StockLog>();

    public virtual Supplier? Supplier { get; set; }
}
