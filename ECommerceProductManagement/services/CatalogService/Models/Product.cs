namespace CatalogService.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string SKU { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public ProductStatus Status { get; set; }

    public List<ProductVariant> Variants { get; set; }
    public List<MediaAsset> MediaAssets { get; set; }
}