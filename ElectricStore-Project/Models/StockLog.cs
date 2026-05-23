using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class StockLog
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? ImportReceiptId { get; set; }

    public int? ExportReceiptId { get; set; }

    public string? RefType { get; set; }

    public int? Delta { get; set; }

    public int? StockBefore { get; set; }

    public int? StockAfter { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? SupplierId { get; set; }

    public virtual ExportReceipt? ExportReceipt { get; set; }

    public virtual ImportReceipt? ImportReceipt { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
