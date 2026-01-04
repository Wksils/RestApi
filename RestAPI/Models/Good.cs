using RestAPI.Models.Abstractions;
using System;
using System.Collections.Generic;

namespace RestAPI.Models;

public partial class Good:CommonObject
{
    public int IdProduct { get; set; }

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int MinimumStock { get; set; }

    public string Sales { get; set; } = null!;

    public virtual ICollection<Sale> SalesNavigation { get; set; } = new List<Sale>();

}
