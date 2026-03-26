namespace CatalogService.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public string PrimaryImageUrl { get; set; } = string.Empty;
    public decimal SellingPrice { get; set; }
    public int StockQuantity { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ProductStatus Status { get; set; }

    public List<ProductVariant> Variants { get; set; } = [];
    public List<MediaAsset> MediaAssets { get; set; } = [];
}
