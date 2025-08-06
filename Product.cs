using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Stage2
{
    public abstract class Product
    {
        public int Id { get; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        protected Product(int id, string name, decimal price)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Product name cannot be empty");
            if (Price < 0)
                throw new ArgumentException("Price cannot be negative.");
            Id = id;
            Name = name;
            Price = price;
        }
        //Method to get product details.

        public virtual string GetDetails()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price:C}";
        }
    }
}
