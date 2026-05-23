using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class PromotionApply
{
    public int Id { get; set; }

    public int? CategoryType { get; set; }

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
