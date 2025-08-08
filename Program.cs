using E_commerce_Stage2;

class Program
{
    static void Main()
    {
        try
        {
            Electronics laptop = new Electronics(1, "Laptop", 1200000.00m, "Lenovo");
            Books thingsFallApart = new Books(1, "ThingFallApart", 24000.00m, "Chinewe Aguchebe");

            Console.WriteLine(laptop.GetDetails() + "\n" + thingsFallApart.GetDetails());
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Input error: {ex.Message}");
        }
        catch (Exception ex)
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


    static string Prompt(string message)
    {
        Console.Write(message);
        string input = Console.ReadLine()?.Trim();

        if (string.Equals(input, "cancel", StringComparison.OrdinalIgnoreCase))
            throw new OperationCanceledException("User cancelled input.");

        return input;
    }


    /// adding a product with input validation.
    static void AddProductUI(InventoryManager inventory)
    {
        try
        {
            string type = Prompt("Enter product type (clothing/electronics/book or 'cancel'): ");
            string name = Prompt("Enter product name: ");

            decimal price;
            while (true)
            {
                string priceInput = Prompt("Enter price: ");
                if (decimal.TryParse(priceInput, out price) && price >= 0) break;
                Console.WriteLine("Invalid price. Please enter a positive number.");
            }

            string specialProperty = Prompt(
                type.ToLower() == "electronics" ? "Enter brand: " :
                type.ToLower() == "clothing" ? "Enter brand: " :
                "Enter author: "
            );

            IProductDetails product = inventory.AddProduct(type, name, price, specialProperty);
            Console.WriteLine($"\nProduct added: {product.GetDetails()}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Action cancelled. Returning to menu...");
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


    try
    {
            int id;
            string inputId = Prompt("Enter product ID to update: ");

            while (true)
            {
                if (int.TryParse(inputId, out id)) break;
                Console.WriteLine("Invalid ID Please enter a valid one.");
            }




            IProductDetails product = inventory.GetProductById(id);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }



            string newName = Prompt("Enter new name:");



            decimal newPrice;
            string inputNewPrice = Prompt("Enter new price:");
            while (true)
            {
                if (decimal.TryParse(inputNewPrice, out newPrice) && newPrice >= 0) break;
                Console.WriteLine("Invalid price. Try again.");
            }

            string newExtra = Prompt(product is Electronics ? "Enter new brand: " : product is Books ? "Enter new author: ":"Enter new brand: ");

            bool updated = inventory.UpdateProduct(id, newName, newPrice, newExtra);
            Console.WriteLine(updated ? "Product updated." : "Update failed.");
        }
    catch (OperationCanceledException)
    {
            Console.WriteLine("Operation was canceled");
        }
    catch (ArgumentException ex)
    {
            Console.WriteLine("Update Update Fatal error");
    }
    
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
