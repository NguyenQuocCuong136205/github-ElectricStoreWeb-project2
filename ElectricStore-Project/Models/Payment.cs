using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
