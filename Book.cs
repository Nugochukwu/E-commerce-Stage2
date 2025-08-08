using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Stage2
{
    public class Books: Product
    {
        public string Author { get; set; }

        public Books(int id, string name, decimal price,string author)
            :base(id,name,price)
        {
            if (string.IsNullOrEmpty(author))
                throw new ArgumentException("Author name cannot be null");
            Author = author;
        }

        //Overriding
        public override string GetDetails() => base.GetDetails()  + $" Author: {Author}";

    }
}
