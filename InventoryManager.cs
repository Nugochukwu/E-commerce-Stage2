using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_commerce_Stage2
{

    /// Manage product inventory CRUD operations.
    public class InventoryManager
    {
        private List<Product> products; // Stores products
        private int idCounter;          // Keeps track of next unique ID

        public InventoryManager()//Initialize Product Details
        {
            products = new List<Product>();
            idCounter = 1;
        }

        /// Add a new product to inventory based on type.
        public Product AddProduct(string type, string name, decimal price, string specialProperty)
        {
            Product newProduct = type.ToLower() switch
            {
                "electronics" => new Electronics(idCounter, name, price, specialProperty),//specialProperty - brand
                "book" or "books" => new Books(idCounter, name, price, specialProperty),//specialProperty - author
                "clothing" => new Clothing(idCounter, name, price,specialProperty),
                _ => throw new ArgumentException("Invalid product type.")
            };

            products.Add(newProduct);
            idCounter++;
            return newProduct;
        }

        /// Return all products in inventory.
        public List<Product> GetAllProducts() => new List<Product>(products);

        /// Find a product by ID or returns null.
        public Product GetProductById(int id) => products.FirstOrDefault(p => p.Id == id);

        /// Update an existing product's details.
        public bool UpdateProduct(int id, string newName, decimal newPrice, string newExtraProperty)
        {
            Product product = GetProductById(id);
            if (product == null) return false;

            product.Name = newName;
            product.Price = newPrice;

            if (product is Electronics electronics)
                electronics.Brand = newExtraProperty;
                
            else if (product is Clothing clothing)
                clothing.Brand = newExtraProperty;
            else if (product is Books book)
                book.Author = newExtraProperty;

            return true;
        }

        /// Deletes a product by ID.
        public bool DeleteProduct(int id)
        {
            Product product = GetProductById(id);
            if (product == null) return false;
            products.Remove(product);
            return true;
        }
    }
}
