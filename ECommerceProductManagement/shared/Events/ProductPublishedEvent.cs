// shared/Events/ProductPublishedEvent.cs
namespace ECommerce.Shared.Events
{
    public record ProductPublishedEvent
    {
        public Guid ProductId { get; init; }
        public string Sku { get; init; }
        public decimal SalePrice { get; init; }
        public DateTime PublishedAt { get; init; }
    }
}
