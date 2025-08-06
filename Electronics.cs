using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Stage2
{
    public class Electronics: Product
    {
        public string Brand { get; set; }

        //Constructor calling base Product Class.
        public Electronics(int id, string name, decimal price, string brand)
            : base(id, name, price)
        {
            if (string.IsNullOrEmpty(brand))
                throw new ArgumentException("Brand name cannot be empty.");
            Brand = brand;
        }

        //Overriding
        public override string GetDetails()
        {
            return base.GetDetails() + $" Brand: {Brand}";
        }
    }
    
}
