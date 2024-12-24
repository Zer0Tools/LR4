using System;
using System.Collections.Generic;

namespace LR4.Models;

public partial class Status
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<SaleHistory> SaleHistories { get; set; } = new List<SaleHistory>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
