using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class ExportReceiptDetail
{
    public int Id { get; set; }

    public int? ExportReceiptId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual ExportReceipt? ExportReceipt { get; set; }

    public virtual Product? Product { get; set; }
}
