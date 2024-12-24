using System;
using System.Collections.Generic;

namespace LR4.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int SourceId { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Source Source { get; set; } = null!;
}
