using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class SpecificationAttribute
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? Name { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ProductSpecificationMapping> ProductSpecificationMappings { get; set; } = new List<ProductSpecificationMapping>();
}
