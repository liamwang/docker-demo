using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerDemo.Models
{
    public class Product
    {
        public Product()
        {
        }

        public Product(string name, string cate, decimal price)
        {
            Name = name;
            Category = cate;
            Price = price;
        }

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
