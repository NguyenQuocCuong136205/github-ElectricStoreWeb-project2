using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class Address
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? FullAddress { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? User { get; set; }
}
