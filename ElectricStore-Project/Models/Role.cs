using System;
using System.Collections.Generic;

namespace ElectricStore_Project.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
