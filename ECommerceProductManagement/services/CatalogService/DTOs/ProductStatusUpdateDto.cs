namespace CatalogService.DTOs;

public class ProductStatusUpdateDto
{
    public string Status { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
}
