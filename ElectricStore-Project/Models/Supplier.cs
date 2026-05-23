using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<StockLog> StockLogs { get; set; } = new List<StockLog>();
}
