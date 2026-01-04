using System;
using System.Collections.Generic;

namespace RestAPI.Models;

public partial class Manufacturer
{
    public int IdManufacturer { get; set; }

    public string ManufacturersName { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
