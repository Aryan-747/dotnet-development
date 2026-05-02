namespace Shared.Events;

public class OrderPlacedEvent
{
    public Guid OrderId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public List<OrderItemEvent> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class OrderItemEvent
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
