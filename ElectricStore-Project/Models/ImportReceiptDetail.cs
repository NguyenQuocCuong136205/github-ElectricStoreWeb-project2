using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class ImportReceiptDetail
{
    public int Id { get; set; }

    public int? ImportReceiptId { get; set; }

    public int? ProductId { get; set; }

    public int? QuantityExpected { get; set; }

    public int? QuantityActual { get; set; }

    public decimal? UnitCost { get; set; }

    public string? Note { get; set; }

    public virtual ImportReceipt? ImportReceipt { get; set; }

    public virtual Product? Product { get; set; }
}
