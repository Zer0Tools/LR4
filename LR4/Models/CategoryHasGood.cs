using System;
using System.Collections.Generic;

namespace LR4.Models;

public partial class CategoryHasGood
{
    public int CategoryId { get; set; }

    public int GoodId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Good Good { get; set; } = null!;
}
