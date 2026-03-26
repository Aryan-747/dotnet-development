using CatalogService.Data;
using CatalogService.Models;

namespace CatalogService.Services;

public static class CatalogSeedService
{
    public static void Seed(CatalogDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Products.Any())
        {
            return;
        }

        context.Products.AddRange(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "AeroFit Running Shoes",
                SKU = "SHOE-001",
                Brand = "AeroFit",
                Description = "Lightweight performance running shoes for everyday training.",
                CategoryName = "Footwear",
                SeoTitle = "AeroFit Running Shoes",
                SeoDescription = "Responsive running shoes with breathable mesh upper.",
                Tags = "running,sports,featured",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 3299,
                StockQuantity = 42,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Nimbus Smart Watch",
                SKU = "WATCH-014",
                Brand = "Nimbus",
                Description = "Smart watch with heart-rate, GPS, and 7-day battery.",
                CategoryName = "Wearables",
                SeoTitle = "Nimbus Smart Watch",
                SeoDescription = "Modern smartwatch with wellness and connectivity features.",
                Tags = "wearable,smartwatch,new",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 8999,
                StockQuantity = 18,
                Status = ProductStatus.ReadyForReview,
                IsPublished = false,
                CreatedAt = DateTime.UtcNow.AddDays(-4),
                UpdatedAt = DateTime.UtcNow.AddHours(-4)
            });

        context.SaveChanges();
    }
}
