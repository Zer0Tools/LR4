using System;
using System.Collections.Generic;

namespace LR4.Models;

public partial class Sale
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string? Number { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public decimal? SaleSum { get; set; }

    public int StatusId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<SaleHistory> SaleHistories { get; set; } = new List<SaleHistory>();

    public virtual Status Status { get; set; } = null!;
}
