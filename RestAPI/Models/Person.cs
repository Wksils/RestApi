using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI.Models;

public partial class Person
{
    public int IdPerson { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Role { get; set; } = null!;
    public string Password { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    [JsonIgnore]
    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
