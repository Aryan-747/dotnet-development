using Microsoft.EntityFrameworkCore;
using CatalogService.Models;

namespace CatalogService.Data
{ 

    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductVariant> Variants { get; set; }
        public DbSet<MediaAsset> MediaAssets { get; set; }
    }
}
