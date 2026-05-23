using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public string? Content { get; set; }

    public decimal? Rating { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual User? User { get; set; }
}
