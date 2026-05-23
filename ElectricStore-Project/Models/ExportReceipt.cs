using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class ExportReceipt
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? CreatedBy { get; set; }

    public string? ExportStatus { get; set; }

    public string? Note { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ExportReceiptDetail> ExportReceiptDetails { get; set; } = new List<ExportReceiptDetail>();

    public virtual Order? Order { get; set; }

    public virtual ICollection<StockLog> StockLogs { get; set; } = new List<StockLog>();
}
