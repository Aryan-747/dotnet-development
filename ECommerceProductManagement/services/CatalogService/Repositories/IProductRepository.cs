using CatalogService.Models;

namespace CatalogService.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task Add(Product product);
}