using System;
using System.Collections.Generic;

namespace RestAPI.Models;

public partial class VendingMachine
{
    public int IdVm { get; set; }

    public string Adres { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Type { get; set; } = null!;

    public decimal Summa { get; set; }

    public string Number { get; set; } = null!;

    public string InventoryNumber { get; set; } = null!;

    public int IdManufacturer { get; set; }

    public DateOnly DateOfManufacture { get; set; }

    public DateOnly DateOfOperation { get; set; }

    public DateOnly DataLastExamination { get; set; }

    public int IntertestInterval { get; set; }

    public int ResoursTa { get; set; }

    public DateOnly DateNextExamination { get; set; }

    public int TimeExamination { get; set; }

    public string Status { get; set; } = null!;

    public int IdCountry { get; set; }

    public DateOnly DateInventory { get; set; }

    public int IdPerson { get; set; }

    public virtual Country IdCountryNavigation { get; set; } = null!;

    public virtual Manufacturer IdManufacturerNavigation { get; set; } = null!;

    public virtual Person IdPersonNavigation { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
