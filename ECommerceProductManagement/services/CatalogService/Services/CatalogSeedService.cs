using CatalogService.Data;
using CatalogService.Models;

namespace CatalogService.Services;

public static class CatalogSeedService
{
    public static void Seed(CatalogDbContext context)
    {
        context.Database.EnsureCreated();

        var now = DateTime.UtcNow;
        var products = BuildProducts(now);
        var existingSkus = context.Products.Select(x => x.SKU).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var newProducts = products.Where(product => !existingSkus.Contains(product.SKU)).ToList();

        if (newProducts.Count == 0)
        {
            return;
        }

        context.Products.AddRange(newProducts);
        context.SaveChanges();
    }

    private static List<Product> BuildProducts(DateTime now)
    {
        return
        [
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
                CreatedAt = now.AddDays(-7),
                UpdatedAt = now.AddDays(-1)
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
                CreatedAt = now.AddDays(-4),
                UpdatedAt = now.AddHours(-4)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Harbor Noise Cancelling Headphones",
                SKU = "AUDIO-021",
                Brand = "Harbor",
                Description = "Over-ear wireless headphones with deep bass and 40-hour playback.",
                CategoryName = "Electronics",
                SeoTitle = "Harbor Noise Cancelling Headphones",
                SeoDescription = "Immersive wireless headphones for work and travel.",
                Tags = "audio,headphones,wireless",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 5499,
                StockQuantity = 26,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-12),
                UpdatedAt = now.AddDays(-2)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Luma Desk Lamp",
                SKU = "HOME-102",
                Brand = "Luma",
                Description = "Adjustable warm-white study lamp with touch dimming.",
                CategoryName = "Home Decor",
                SeoTitle = "Luma Desk Lamp",
                SeoDescription = "Modern dimmable desk lamp for focused workspaces.",
                Tags = "home,lighting,desk",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 1899,
                StockQuantity = 64,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-10),
                UpdatedAt = now.AddDays(-3)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Northwind Travel Backpack",
                SKU = "BAG-310",
                Brand = "Northwind",
                Description = "Water-resistant commuter backpack with padded laptop sleeve.",
                CategoryName = "Bags",
                SeoTitle = "Northwind Travel Backpack",
                SeoDescription = "Daily carry backpack for office, campus, and weekend trips.",
                Tags = "bags,travel,laptop",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 2799,
                StockQuantity = 33,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-9),
                UpdatedAt = now.AddDays(-2)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "BrewCraft Pour Over Set",
                SKU = "KITCH-078",
                Brand = "BrewCraft",
                Description = "Glass dripper starter set for manual coffee brewing.",
                CategoryName = "Kitchen",
                SeoTitle = "BrewCraft Pour Over Set",
                SeoDescription = "Premium pour over coffee set for slow mornings.",
                Tags = "kitchen,coffee,premium",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 2499,
                StockQuantity = 21,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-8),
                UpdatedAt = now.AddDays(-1)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Studio Mechanical Keyboard",
                SKU = "TECH-501",
                Brand = "StudioKey",
                Description = "Compact wireless keyboard with tactile switches and RGB backlight.",
                CategoryName = "Computers",
                SeoTitle = "Studio Mechanical Keyboard",
                SeoDescription = "Responsive keyboard designed for creators and coders.",
                Tags = "keyboard,computers,workspace",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1511467687858-23d96c32e4ae?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 4599,
                StockQuantity = 17,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-6),
                UpdatedAt = now.AddDays(-1)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Evergreen Indoor Plant Pot",
                SKU = "HOME-214",
                Brand = "Evergreen",
                Description = "Ceramic planter with drain tray for small indoor greens.",
                CategoryName = "Home Decor",
                SeoTitle = "Evergreen Indoor Plant Pot",
                SeoDescription = "Minimal ceramic plant pot for desks and shelves.",
                Tags = "plants,decor,home",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1485955900006-10f4d324d411?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 999,
                StockQuantity = 95,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-5),
                UpdatedAt = now.AddDays(-1)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Pulse Yoga Mat",
                SKU = "FIT-120",
                Brand = "Pulse",
                Description = "Non-slip high-density yoga mat for studio and home sessions.",
                CategoryName = "Fitness",
                SeoTitle = "Pulse Yoga Mat",
                SeoDescription = "Comfortable workout mat with superior floor grip.",
                Tags = "fitness,yoga,wellness",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1518611012118-696072aa579a?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 1599,
                StockQuantity = 48,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-4),
                UpdatedAt = now.AddDays(-1)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Astra 4K Monitor",
                SKU = "TECH-612",
                Brand = "Astra",
                Description = "27-inch UHD monitor with HDR support and slim bezels.",
                CategoryName = "Electronics",
                SeoTitle = "Astra 4K Monitor",
                SeoDescription = "Sharp 4K monitor built for gaming and productivity.",
                Tags = "monitor,4k,electronics",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 21999,
                StockQuantity = 9,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-3),
                UpdatedAt = now.AddHours(-18)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coastline Linen Shirt",
                SKU = "APP-880",
                Brand = "Coastline",
                Description = "Relaxed-fit linen shirt for warm days and layered evenings.",
                CategoryName = "Fashion",
                SeoTitle = "Coastline Linen Shirt",
                SeoDescription = "Breathable casual linen shirt with soft drape.",
                Tags = "fashion,linen,summer",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 2299,
                StockQuantity = 57,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-2),
                UpdatedAt = now.AddHours(-10)
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Terra Ceramic Dinner Set",
                SKU = "DIN-404",
                Brand = "Terra",
                Description = "Twelve-piece stoneware dinner set with matte finish.",
                CategoryName = "Kitchen",
                SeoTitle = "Terra Ceramic Dinner Set",
                SeoDescription = "Elegant ceramic dining essentials for modern homes.",
                Tags = "kitchen,dining,ceramic",
                PrimaryImageUrl = "https://images.unsplash.com/photo-1496417263034-38ec4f0b665a?auto=format&fit=crop&w=1200&q=80",
                SellingPrice = 3899,
                StockQuantity = 23,
                Status = ProductStatus.Published,
                IsPublished = true,
                CreatedAt = now.AddDays(-1),
                UpdatedAt = now.AddHours(-6)
            }
        ];
    }
}
