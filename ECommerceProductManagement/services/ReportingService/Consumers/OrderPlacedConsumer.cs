using MassTransit;
using Shared.Events;
using ReportingService.Data;
using ReportingService.Models;

namespace ReportingService.Consumers;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly ReportingDbContext _context;
    private readonly ILogger<OrderPlacedConsumer> _logger;

    public OrderPlacedConsumer(ReportingDbContext context, ILogger<OrderPlacedConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Consuming OrderPlacedEvent for Audit: OrderId {OrderId}", message.OrderId);

        foreach (var item in message.Items)
        {
            var log = new AuditLog
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Action = "Order Placed",
                EntityName = "Order",
                Details = $"Order #{message.OrderId} placed asynchronously. Quantity: {item.Quantity} units. Price: ₹{item.UnitPrice}",
                ActorEmail = message.UserEmail,
                CreatedAt = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Audit logs saved for Order {OrderId}", message.OrderId);
    }
}
