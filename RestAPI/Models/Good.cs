using Common.Models;
using RestAPI.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestAPI.Models;

public partial class Good //:CommonObject
{
    //public Good()
    //{
    //}

    //public Good(GoodModel model) : base(model)
    //{
    //    IdProduct = model.Id;
    //    ProductName = model.ProductName;
    //    Description = model.Description;
    //    Price = model.Price;
    //    Quantity = model.Quantity;
    //    MinimumStock = model.MinimumStock;
    //    Sales = model.Sales;
    //    Photo = model.Photo;

    //}
    //public GoodModel ToDTO()
    //{
    //    return new GoodModel()
    //    {
    //        Id = this.IdProduct,
    //        ProductName = this.ProductName,
    //        Description = this.Description,
    //        Price = this.Price,
    //        Quantity = this.Quantity,
    //        MinimumStock = this.MinimumStock,
    //        Sales = this.Sales,
    //        Photo = this.Photo
    //    };
    //}
    //public GoodModel ToShortDTO()
    //{
    //    return new GoodModel()
    //    {
    //        Id = this.IdProduct,
    //        Name = this.ProductName,
    //        Description = this.Description,
    //        Photo = this.Photo,
    //        CreationDate = this.CreationDate
    //    };
    //}

    public int IdProduct { get; set; }

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int MinimumStock { get; set; }

    public string Sales { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Sale> SalesNavigation { get; set; } = new List<Sale>();

}
