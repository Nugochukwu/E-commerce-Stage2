using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Stage2
{
    public interface IProductDetails
    {
        int Id { get; }
        string Name { get; set; }
        decimal Price { get; set; }
        string GetDetails();
    }

   
}
