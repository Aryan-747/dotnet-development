using TestApp.Models;
using System.Collections.Generic;

namespace TestApp.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        void Add(Product product);
        Product GetProductById(int id);
    }
}