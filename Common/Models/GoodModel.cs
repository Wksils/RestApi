using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class GoodModel : CommonModel
    {
        public GoodModel()
        {

        }

        public GoodModel(string productName, string description, decimal price, int quantity, int minimumStock, string sales)
        {
            ProductName = productName;
            Description = description;
            Price = price;
            Quantity = quantity;
            MinimumStock = minimumStock;
            Sales = sales;
        }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int MinimumStock { get; set;}
        public string Sales { get; set; }
        
    }
}
