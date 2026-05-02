using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using System.Security.Claims;
using MassTransit;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrdersController(OrderDbContext context, IHttpClientFactory httpClientFactory, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient();
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var orders = await _context.Orders
            .Include(o => o.Items)
            .Include(o => o.ShippingAddress)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Include(o => o.ShippingAddress)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .Include(o => o.ShippingAddress)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();

        // Check if user is admin or the owner
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var role = User.FindFirstValue(ClaimTypes.Role);

        if (role != "Admin" && order.UserId != userId)
        {
            return Forbid();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] Order orderRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? "unknown";
        if (userId == null) return Unauthorized();

        orderRequest.Id = Guid.NewGuid();
        orderRequest.UserId = userId;
        orderRequest.UserEmail = userEmail;
        orderRequest.CreatedAt = DateTime.UtcNow;
        orderRequest.UpdatedAt = DateTime.UtcNow;
        orderRequest.Status = "Placed";

        decimal total = 0;
        foreach(var item in orderRequest.Items)
        {
            item.Id = Guid.NewGuid();
            item.OrderId = orderRequest.Id;
            total += item.UnitPrice * item.Quantity;
        }
        orderRequest.TotalAmount = total;

        _context.Orders.Add(orderRequest);
        await _context.SaveChangesAsync();

        // Asynchronous Event Publication (RabbitMQ)
        var orderEvent = new Shared.Events.OrderPlacedEvent
        {
            OrderId = orderRequest.Id,
            UserId = userId,
            UserEmail = userEmail,
            CreatedAt = orderRequest.CreatedAt,
            Items = orderRequest.Items.Select(i => new Shared.Events.OrderItemEvent
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        await _publishEndpoint.Publish(orderEvent);

        return Ok(orderRequest);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateStatusDto dto)
    {
        var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return NotFound();

        var oldStatus = order.Status;
        order.Status = dto.Status;
        order.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Audit for each item
        var authHeader = Request.Headers["Authorization"].ToString();
        foreach (var item in order.Items)
        {
            try
            {
                var auditDto = new
                {
                    ProductId = item.ProductId,
                    Action = "Status Updated",
                    EntityName = "Order",
                    Details = $"Order #{order.Id} status changed from '{oldStatus}' to '{dto.Status}'."
                };
                var auditRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5004/api/reports/audit");
                auditRequest.Headers.Add("Authorization", authHeader);
                auditRequest.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(auditDto), System.Text.Encoding.UTF8, "application/json");
                await _httpClient.SendAsync(auditRequest);
            }
            catch { /* Best effort */ }
        }

        return Ok(order);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return NotFound();

        // Audit for each item BEFORE deletion
        var authHeader = Request.Headers["Authorization"].ToString();
        foreach (var item in order.Items)
        {
            try
            {
                var auditDto = new
                {
                    ProductId = item.ProductId,
                    Action = "Order Deleted",
                    EntityName = "Order",
                    Details = $"Admin deleted Order #{order.Id} containing this product."
                };
                var auditRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5004/api/reports/audit");
                auditRequest.Headers.Add("Authorization", authHeader);
                auditRequest.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(auditDto), System.Text.Encoding.UTF8, "application/json");
                await _httpClient.SendAsync(auditRequest);
            }
            catch { /* Best effort */ }
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        
        if (order == null) return NotFound();
        if (order.UserId != userId) return Forbid();
        if (order.Status != "Placed") return BadRequest("Only orders in 'Placed' status can be cancelled.");

        order.Status = "Cancelled";
        order.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();

        // Audit for each item
        var authHeader = Request.Headers["Authorization"].ToString();
        foreach (var item in order.Items)
        {
            try
            {
                var auditDto = new
                {
                    ProductId = item.ProductId,
                    Action = "Order Cancelled",
                    EntityName = "Order",
                    Details = $"Order #{order.Id} cancelled by customer."
                };
                var auditRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5004/api/reports/audit");
                auditRequest.Headers.Add("Authorization", authHeader);
                auditRequest.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(auditDto), System.Text.Encoding.UTF8, "application/json");
                await _httpClient.SendAsync(auditRequest);
            }
            catch { /* Best effort */ }
        }

        return Ok(order);
    }
}

public class UpdateStatusDto
{
    public string Status { get; set; } = string.Empty;
}
