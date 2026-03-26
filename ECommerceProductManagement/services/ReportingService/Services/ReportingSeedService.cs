using ReportingService.Data;
using ReportingService.Models;

namespace ReportingService.Services;

public static class ReportingSeedService
{
    public static void Seed(ReportingDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.AuditLogs.Any())
        {
            return;
        }

        context.AuditLogs.AddRange(
            new AuditLog
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Action = "Seeded",
                ActorEmail = "system@ecommerce.local",
                EntityName = "Catalog",
                Details = "Initial dashboard activity created.",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new AuditLog
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Action = "PendingReview",
                ActorEmail = "pm@ecommerce.local",
                EntityName = "Workflow",
                Details = "Product submitted for review.",
                CreatedAt = DateTime.UtcNow.AddHours(-8)
            });

        context.SaveChanges();
    }
}
