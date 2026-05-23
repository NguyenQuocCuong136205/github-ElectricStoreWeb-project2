using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class ImportReceipt
{
    public int Id { get; set; }

    public string? ReferenceCode { get; set; }

    public int? CreatedBy { get; set; }

    public string? ImportStatus { get; set; }

    public string? Note { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public string? SourceFile { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ImportReceiptDetail> ImportReceiptDetails { get; set; } = new List<ImportReceiptDetail>();

    public virtual ICollection<StockLog> StockLogs { get; set; } = new List<StockLog>();
}
