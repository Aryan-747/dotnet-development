using DTOs;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Mvc;
using IdentityService.Services;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
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
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            PasswordHash = dto.Password, // later hash
            Role = dto.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Email == dto.Email && x.PasswordHash == dto.Password);

        if (user == null)
            return Unauthorized();

        var token = _tokenService.CreateToken(user);

        return Ok(token);
    }
}