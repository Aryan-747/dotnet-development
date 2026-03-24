namespace CatalogService.Models;

public enum ProductStatus
{
    Draft,
    InEnrichment,
    ReadyForReview,
    Approved,
    Published,
    Rejected,
    Archived
}