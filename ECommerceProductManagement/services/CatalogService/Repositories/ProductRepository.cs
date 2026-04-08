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

    public async Task<List<Product>> SearchPublished(string? query, string? category, string? sort)
    {
        var products = _context.Products
            .Where(x => x.IsPublished);

        if (!string.IsNullOrWhiteSpace(query))
        {
            var normalizedQuery = query.Trim().ToLower();

            products = products.Where(product =>
                product.Name.ToLower().Contains(normalizedQuery) ||
                product.Brand.ToLower().Contains(normalizedQuery) ||
                product.CategoryName.ToLower().Contains(normalizedQuery) ||
                product.Tags.ToLower().Contains(normalizedQuery) ||
                product.SKU.ToLower().Contains(normalizedQuery));
        }

        if (!string.IsNullOrWhiteSpace(category) &&
            !string.Equals(category, "All", StringComparison.OrdinalIgnoreCase))
        {
            products = products.Where(product => product.CategoryName == category);
        }

        products = (sort ?? string.Empty).ToLowerInvariant() switch
        {
            "price-low" => products.OrderBy(product => product.SellingPrice),
            "price-high" => products.OrderByDescending(product => product.SellingPrice),
            "newest" => products.OrderByDescending(product => product.UpdatedAt),
            _ => products.OrderByDescending(product => product.UpdatedAt)
        };

        return await products.ToListAsync();
    }

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
