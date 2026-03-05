using ConsoleApp.Models;
using ConsoleApp.Repositories;
using System;

class Program
{
    static void Main()
    {
        IProductRepository repo = new ProductRepository();

        repo.Add(new Product { Id = 1, Name = "Laptop", Price = 80000 });
        repo.Add(new Product { Id = 2, Name = "Phone", Price = 30000 });

        foreach (var product in repo.GetAll())
        {
            Console.WriteLine($"{product.Id} {product.Name} {product.Price}");
        }
    }
}