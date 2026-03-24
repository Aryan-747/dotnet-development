using CatalogService.Data;
using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _context;

    public ProductRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAll()
        => await _context.Products.ToListAsync();

    public async Task<Product> GetById(Guid id)
        => await _context.Products.FindAsync(id);

    public async Task Add(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }
}