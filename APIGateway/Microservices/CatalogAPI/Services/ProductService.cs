using CatalogAPI.Models;

namespace CatalogAPI.Services
{
    public class ProductService
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 80000 },
            new Product { Id = 2, Name = "Mobile", Price = 30000 },
            new Product { Id = 3, Name = "Headphones", Price = 2000 }
        };

        public List<Product> GetProducts()
        {
            return products;
        }

        public Product GetProduct(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }
    }
}