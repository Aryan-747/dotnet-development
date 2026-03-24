namespace CatalogService.Models;

public class MediaAsset
{
    public Guid Id { get; set; }
    public string Url { get; set; }

    public Guid ProductId { get; set; }
}