using MassTransit;
using Shared.Events;
using CatalogService.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Consumers;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<OrderPlacedConsumer> _logger;

    public OrderPlacedConsumer(CatalogDbContext context, ILogger<OrderPlacedConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Consuming OrderPlacedEvent for OrderId: {OrderId}", message.OrderId);

        foreach (var item in message.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product != null)
            {
                _logger.LogInformation("Reducing stock for Product: {ProductId}, Qty: {Qty}", item.ProductId, item.Quantity);
                product.StockQuantity -= item.Quantity;
                product.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                _logger.LogWarning("Product {ProductId} not found in catalog for Order {OrderId}", item.ProductId, message.OrderId);
            }
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Stock updated successfully for Order {OrderId}", message.OrderId);
    }
}
