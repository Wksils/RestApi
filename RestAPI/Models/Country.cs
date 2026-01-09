using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI.Models;

public partial class Country
{
    public int IdCountry { get; set; }

    public string Country1 { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
