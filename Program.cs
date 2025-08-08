using E_commerce_Stage2;

class Program
{
    static void Main()
    {
        try
        {
            Electronics laptop = new Electronics(1, "Laptop", 1200000.00m, "Lenovo");
            Books thingsFallApart = new Books(1, "ThingFallApart", 24000.00m, "Chinewe Aguchebe");
        
            Console.WriteLine(laptop.GetDetails()+"\n"+thingsFallApart.GetDetails());
        }
        catch(ArgumentException ex)
        {
            Console.WriteLine($"Input error: { ex.Message}");
        }
        catch(Exception ex) 
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }


                                    // Console Shenanigans //



        InventoryManager inventory = new InventoryManager();
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("<= Inventory Manager =>");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. View Product by ID");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddProductUI(inventory); break;
                case "2": ViewAllProducts(inventory); break;
                case "3": ViewProductByIdUI(inventory); break;
                case "4": UpdateProductUI(inventory); break;
                case "5": DeleteProductUI(inventory); break;
                case "6": running = false; break;
                default: Console.WriteLine("Invalid option."); break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to return to menu...");
                Console.ReadLine();
            }
        }
    }


                                    // Console UI //


    /// adding a product with input validation.
    static void AddProductUI(InventoryManager inventory)
    {
        Console.Write("Enter product type (clothing/electronics/book): ");
        string type = Console.ReadLine()?.Trim();

        Console.Write("Enter product name: ");
        string name = Console.ReadLine()?.Trim();

        decimal price;
        while (true)
        {
            Console.Write("Enter price: ");
            if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0) break;
            Console.WriteLine("Invalid price. Please enter a positive number.");
        }

        Console.Write(
            type.ToLower() == "electronics" ? "Enter brand: " :
            type.ToLower() == "clothing" ? "Enter brand: " :
            "Enter author: "
        );
        string specialProperty = Console.ReadLine()?.Trim();

        try
        {
            //declare product as a variable of the data type "Product(The class containing the object blue prints for each individual product item.)"
            IProductDetails product = inventory.AddProduct(type, name, price, specialProperty);
            Console.WriteLine($"\nProduct added: {product.GetDetails()}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nCould not add product: {ex.Message}");
        }
    }



    /// Display all products.
    static void ViewAllProducts(InventoryManager inventory)
    {
        List<IProductDetails> products = inventory.GetAllProducts();
        if (products.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("\n=== Current Inventory ===");
        foreach (var product in products)
            Console.WriteLine(product.GetDetails());
    }



    /// Display a single product by ID.
    static void ViewProductByIdUI(InventoryManager inventory)
    {
        Console.Write("Enter product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        IProductDetails product = inventory.GetProductById(id);
        if (product == null) Console.WriteLine("Product not found.");
        else Console.WriteLine(product.GetDetails());
    }
    //Changes have been made


    /// Update a product by ID.
    static void UpdateProductUI(InventoryManager inventory)
    {
        Console.Write("Enter product ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }



        IProductDetails product = inventory.GetProductById(id);
        
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }



        Console.Write("Enter new name: ");
        string newName = Console.ReadLine()?.Trim();



        decimal newPrice;
        while (true)
        {
            Console.Write("Enter new price: ");
            if (decimal.TryParse(Console.ReadLine(), out newPrice) && newPrice >= 0) break;
            Console.WriteLine("Invalid price. Try again.");
        }

        string newExtra;
        Console.Write(product is Electronics ? "Enter new brand: " : "Enter new author: ");
        newExtra = Console.ReadLine()?.Trim();

        bool updated = inventory.UpdateProduct(id, newName, newPrice, newExtra);
        Console.WriteLine(updated ? "Product updated." : "Update failed.");
    }



    /// Delete a product by ID.
    static void DeleteProductUI(InventoryManager inventory)
    {
        Console.Write("Enter product ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        bool deleted = inventory.DeleteProduct(id);
        Console.WriteLine(deleted ? "Product deleted." : "Product not found.");

    }
}
