namespace CatalogService.Models;

public class ProductVariant
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
