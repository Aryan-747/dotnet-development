using MassTransit;
using ECommerce.Shared.Events;

namespace CatalogService.Consumers
{
    public class ProductPublishedConsumer : IConsumer<ProductPublishedEvent>
    {
        public async Task Consume(ConsumeContext<ProductPublishedEvent> context)
        {
            var data = context.Message;
            Console.WriteLine($"[RabbitMQ] Received Event! Product {data.ProductId} (SKU: {data.Sku}) was published for ${data.SalePrice}");
            
            // TODO: Update the CatalogDbContext to make it visible to customers
            await Task.CompletedTask;
        }
    }
}
