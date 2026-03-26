using DTOs;
using IdentityService.Data;
using IdentityService.DTOs;
using IdentityService.Models;
using IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private static readonly string[] AllowedRoles = ["Admin", "ProductManager", "ContentExecutive"];

    private readonly IdentityDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(IdentityDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) ||
            string.IsNullOrWhiteSpace(dto.Email) ||
            string.IsNullOrWhiteSpace(dto.Password) ||
            string.IsNullOrWhiteSpace(dto.Role))
        {
            return BadRequest("All fields are required.");
        }

        if (!AllowedRoles.Contains(dto.Role))
        {
            return BadRequest("Invalid role selected.");
        }

        if (_context.Users.Any(x => x.Email == dto.Email))
        {
            return Conflict("Email is already registered.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = dto.Password,
            Role = dto.Role,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(ToProfile(user));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Email == dto.Email && x.PasswordHash == dto.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(new AuthResponseDto
        {
            Token = _tokenService.CreateToken(user),
            User = ToProfile(user)
        });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = _context.Users.FirstOrDefault(x => x.Email == email);

        return user == null ? NotFound() : Ok(ToProfile(user));
    }

    private static UserProfileDto ToProfile(User user)
    {
        return new UserProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}
