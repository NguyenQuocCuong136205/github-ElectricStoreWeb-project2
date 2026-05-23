using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SpecificationAttribute> SpecificationAttributes { get; set; } = new List<SpecificationAttribute>();
}
