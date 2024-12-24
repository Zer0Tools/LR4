using System;
using System.Collections.Generic;

namespace LR4.Models;

public partial class SaleHasGood
{
    public int SaleId { get; set; }

    public int GoodId { get; set; }

    public virtual Good Good { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
