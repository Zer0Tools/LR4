using System;
using System.Collections.Generic;

namespace LR4.Models;

public partial class Source
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
