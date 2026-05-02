using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using System.Security.Claims;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AddressesController : ControllerBase
{
    private readonly OrderDbContext _context;

    public AddressesController(OrderDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyAddresses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var addresses = await _context.UserAddresses
            .Where(a => a.UserId == userId)
            .ToListAsync();

        return Ok(addresses);
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress([FromBody] UserAddress address)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        address.UserId = userId;
        address.Id = Guid.NewGuid();
        
        // If it's the first address or set to default, make others not default
        var existingCount = await _context.UserAddresses.CountAsync(a => a.UserId == userId);
        if (existingCount == 0) address.IsDefault = true;

        if (address.IsDefault)
        {
            var existing = await _context.UserAddresses.Where(a => a.UserId == userId).ToListAsync();
            foreach (var a in existing) a.IsDefault = false;
        }

        _context.UserAddresses.Add(address);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMyAddresses), new { id = address.Id }, address);
    }
}
