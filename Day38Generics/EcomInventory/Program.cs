using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceInventorySystem
{
    // ===================== 1. BASE INTERFACE =====================
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        decimal Price { get; set; }
        Category Category { get; }
    }

    public enum Category {Electronics,Clothing,Books,Groceries}

    // 2. GENERIC REPOSITORY
    public class ProductRepository<T> where T : class, IProduct
    {
        private List<T> _products = new List<T>();

        public void AddProduct(T product)
        {
            if (product == null)
                throw new ArgumentNullException("Product cannot be null");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty");

            if (product.Price <= 0)
                throw new ArgumentException("Price must be positive");

            if (_products.Any(p => p.Id == product.Id))
                throw new ArgumentException("Product ID must be unique");

            _products.Add(product);
        }

        public IEnumerable<T> FindProducts(Func<T, bool> predicate)
        {
            return _products.Where(predicate);
        }

        public decimal CalculateTotalValue()
        {
            return _products.Sum(p => p.Price);
        }

        public List<T> GetAll()
        {
            return _products;
        }
    }

    // 3. ELECTRONIC PRODUCT
    public class ElectronicProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category => Category.Electronics;

        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }
    }

    // 4. DISCOUNT WRAPPER
    public class DiscountedProduct<T> where T : IProduct
    {
        private T _product;
        private decimal _discountPercentage;

        public DiscountedProduct(T product, decimal discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Discount must be between 0 and 100");

            _product = product;
            _discountPercentage = discountPercentage;
        }

        public decimal DiscountedPrice =>
            _product.Price * (1 - _discountPercentage / 100);

        public override string ToString()
        {
            return $"{_product.Name} | Original: {_product.Price} | Discounted: {DiscountedPrice}";
        }
    }

    // 5. INVENTORY MANAGER
    public class InventoryManager
    {
        public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
        {
            Console.WriteLine("\n--- All Products ---");
            foreach (var p in products)
                Console.WriteLine($"{p.Name} - ₹{p.Price}");

            var expensive = products.OrderByDescending(p => p.Price).First();
            Console.WriteLine($"\nMost Expensive Product: {expensive.Name}");

            Console.WriteLine("\n--- Grouped By Category ---");
            var groups = products.GroupBy(p => p.Category);
            foreach (var group in groups)
            {
                Console.WriteLine(group.Key);
                foreach (var p in group)
                    Console.WriteLine($"  {p.Name}");
            }

            Console.WriteLine("\nApplying 10% discount to Electronics over ₹500");
            foreach (var p in products.Where(p => p.Category == Category.Electronics && p.Price > 500))
            {
                p.Price *= 0.9m;
            }
        }

        public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster)
            where T : IProduct
        {
            foreach (var product in products)
            {
                try
                {
                    product.Price = priceAdjuster(product);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating {product.Name}: {ex.Message}");
                }
            }
        }
    }

    // 6. MAIN TEST PROGRAM
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new ProductRepository<ElectronicProduct>();

            // Adding Products
            repo.AddProduct(new ElectronicProduct { Id = 1, Name = "Laptop", Price = 80000, Brand = "Dell", WarrantyMonths = 24 });
            repo.AddProduct(new ElectronicProduct { Id = 2, Name = "Smartphone", Price = 60000, Brand = "Samsung", WarrantyMonths = 12 });
            repo.AddProduct(new ElectronicProduct { Id = 3, Name = "Headphones", Price = 3000, Brand = "Sony", WarrantyMonths = 6 });
            repo.AddProduct(new ElectronicProduct { Id = 4, Name = "Monitor", Price = 12000, Brand = "LG", WarrantyMonths = 18 });
            repo.AddProduct(new ElectronicProduct { Id = 5, Name = "Keyboard", Price = 1500, Brand = "Logitech", WarrantyMonths = 12 });

            Console.WriteLine("Total Inventory Value (Before Discount): ₹" + repo.CalculateTotalValue());

            // Find by Brand
            Console.WriteLine("\nSony Products:");
            foreach (var p in repo.FindProducts(p => p.Brand == "Sony"))
                Console.WriteLine(p.Name);

            // Discount Wrapper Demo
            var discounted = new DiscountedProduct<ElectronicProduct>(repo.GetAll()[0], 15);
            Console.WriteLine("\nDiscount Example:");
            Console.WriteLine(discounted);

            // Inventory Manager
            var manager = new InventoryManager();
            manager.ProcessProducts(repo.GetAll());

            Console.WriteLine("\nTotal Inventory Value (After Discount): ₹" + repo.CalculateTotalValue());

            // Bulk Price Update
            manager.UpdatePrices(repo.GetAll(), p => p.Price + 100);

            Console.WriteLine("\nAfter Bulk Price Update:");
            foreach (var p in repo.GetAll())
                Console.WriteLine($"{p.Name} - ₹{p.Price}");
        }
    }
}
