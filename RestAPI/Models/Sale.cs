using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI.Models;

public partial class Sale
{
    public int IdSales { get; set; }

    public int IdVm { get; set; }

    public int IdProduct { get; set; }

    public int SoldProduct { get; set; }

    public decimal SummaSale { get; set; }

    public DateTime DateTimeSale { get; set; }

    public string PaymentMethod { get; set; } = null!;
    [JsonIgnore]
    public virtual Good IdProductNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual VendingMachine IdVmNavigation { get; set; } = null!;
}
