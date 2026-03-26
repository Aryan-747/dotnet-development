namespace CatalogService.Models;

public class MediaAsset
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string AltText { get; set; } = string.Empty;

    public Guid ProductId { get; set; }
}
