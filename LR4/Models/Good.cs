using System;
using System.Collections.Generic;

namespace LR4.Models;

public partial class Good
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }
}
