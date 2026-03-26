using IdentityService.Data;
using IdentityService.Models;

namespace IdentityService.Services;

public static class IdentitySeedService
{
    public static void Seed(IdentityDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;
        }

        context.Users.AddRange(
            new User
            {
                Id = Guid.NewGuid(),
                Name = "System Admin",
                Email = "admin@ecommerce.local",
                PasswordHash = "Admin@123",
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Mira Product",
                Email = "pm@ecommerce.local",
                PasswordHash = "Product@123",
                Role = "ProductManager",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Chris Content",
                Email = "content@ecommerce.local",
                PasswordHash = "Content@123",
                Role = "ContentExecutive",
                CreatedAt = DateTime.UtcNow
            });

        context.SaveChanges();
    }
}
