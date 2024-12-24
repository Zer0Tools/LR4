using System;
using System.Collections.Generic;

namespace LR4.Models;

public partial class SaleHistory
{
    public int Id { get; set; }

    public int SaleId { get; set; }

    public int StatusId { get; set; }

    public decimal? SaleSum { get; set; }

    public DateTime ActiveFrom { get; set; }

    public DateTime ActiveTo { get; set; }

    public virtual Sale Sale { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
