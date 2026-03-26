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
        => await _context.Products
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync();

    public async Task<List<Product>> GetPublished()
        => await _context.Products
            .Where(x => x.IsPublished)
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync();

    public async Task<Product> GetById(Guid id)
        => await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

    public async Task Add(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}
