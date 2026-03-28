using CatalogService.Models;

namespace CatalogService.Repositories;
public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<List<Product>> GetPublished();
    Task<Product> GetById(Guid id);
    Task Update(Product product);
    Task Add(Product product);
}
