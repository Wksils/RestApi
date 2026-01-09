using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI.Models;

public partial class Service
{
    public int IdService { get; set; }

    public int IdVm { get; set; }

    public DateOnly DateService { get; set; }

    public string DescriptionService { get; set; } = null!;

    public string? Problem { get; set; }

    public int IdPerson { get; set; }
    [JsonIgnore]
    public virtual Person IdPersonNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual VendingMachine IdVmNavigation { get; set; } = null!;
}
