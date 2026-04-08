using CatalogService.Models;

namespace CatalogService.Repositories;
public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<List<Product>> GetPublished();
    Task<List<Product>> SearchPublished(string? query, string? category, string? sort);
    Task<Product> GetById(Guid id);
    Task Update(Product product);
    Task Add(Product product);
}
