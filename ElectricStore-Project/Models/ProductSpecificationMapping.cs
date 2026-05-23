using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class ProductSpecificationMapping
{
    public int ProductId { get; set; }

    public int AttributeId { get; set; }

    public string? Value { get; set; }

    public virtual SpecificationAttribute Attribute { get; set; } = null!;
}
