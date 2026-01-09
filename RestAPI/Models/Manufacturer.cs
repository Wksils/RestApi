using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI.Models;

public partial class Manufacturer
{
    public int IdManufacturer { get; set; }

    public string ManufacturersName { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
