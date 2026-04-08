using DTOs;
using Google.Apis.Auth;
using IdentityService.Data;
using IdentityService.DTOs;
using IdentityService.Models;
using IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private static readonly string[] AllowedRoles = ["Admin", "ProductManager", "ContentExecutive"];

    private readonly IdentityDbContext _context;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthController(
        IdentityDbContext context,
        TokenService tokenService,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
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
            CreatedAt = DateTime.UtcNow,
            AuthProvider = "Local"
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

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.IdToken))
        {
            return BadRequest("Google ID token is required.");
        }

        var clientId = _configuration["GoogleAuth:ClientId"];
        var defaultRole = _configuration["GoogleAuth:DefaultRole"] ?? "ContentExecutive";

        if (string.IsNullOrWhiteSpace(clientId))
        {
            return StatusCode(500, "Google client ID is not configured.");
        }

        if (!AllowedRoles.Contains(defaultRole))
        {
            return StatusCode(500, "Configured Google default role is invalid.");
        }

        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                dto.IdToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = [clientId]
                });

            var response = await BuildGoogleAuthResponse(payload, defaultRole);
            return Ok(response);
        }
        catch
        {
            return Unauthorized("Invalid Google token.");
        }
    }

    [HttpGet("google/start")]
    public IActionResult GoogleStart()
    {
        var clientId = _configuration["GoogleAuth:ClientId"];
        var redirectUri = _configuration["GoogleAuth:RedirectUri"];

        if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(redirectUri))
        {
            return StatusCode(500, "Google OAuth configuration is incomplete.");
        }

        var authorizationUrl =
            "https://accounts.google.com/o/oauth2/v2/auth" +
            $"?client_id={Uri.EscapeDataString(clientId)}" +
            $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
            "&response_type=code" +
            $"&scope={Uri.EscapeDataString("openid email profile")}" +
            "&access_type=offline" +
            "&prompt=select_account";

        return Redirect(authorizationUrl);
    }

    [HttpGet("google/callback")]
    public async Task<IActionResult> GoogleCallback([FromQuery] string? code, [FromQuery] string? error)
    {
        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest($"Google authorization failed: {error}");
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            return BadRequest("Google authorization code is missing.");
        }

        var clientId = _configuration["GoogleAuth:ClientId"];
        var clientSecret = _configuration["GoogleAuth:ClientSecret"];
        var redirectUri = _configuration["GoogleAuth:RedirectUri"];
        var defaultRole = _configuration["GoogleAuth:DefaultRole"] ?? "ContentExecutive";

        if (string.IsNullOrWhiteSpace(clientId) ||
            string.IsNullOrWhiteSpace(clientSecret) ||
            string.IsNullOrWhiteSpace(redirectUri))
        {
            return StatusCode(500, "Google OAuth configuration is incomplete.");
        }

        if (!AllowedRoles.Contains(defaultRole))
        {
            return StatusCode(500, "Configured Google default role is invalid.");
        }

        var httpClient = _httpClientFactory.CreateClient();
        var tokenResponse = await httpClient.PostAsync(
            "https://oauth2.googleapis.com/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = code,
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["redirect_uri"] = redirectUri,
                ["grant_type"] = "authorization_code"
            }));

        if (!tokenResponse.IsSuccessStatusCode)
        {
            var responseBody = await tokenResponse.Content.ReadAsStringAsync();
            return BadRequest($"Google token exchange failed: {responseBody}");
        }

        var tokenPayload = await tokenResponse.Content.ReadFromJsonAsync<GoogleTokenResponse>();

        if (string.IsNullOrWhiteSpace(tokenPayload?.IdToken))
        {
            return BadRequest("Google token response did not include an ID token.");
        }

        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                tokenPayload.IdToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = [clientId]
                });

            var response = await BuildGoogleAuthResponse(payload, defaultRole);
            return Ok(response);
        }
        catch
        {
            return Unauthorized("Invalid Google token.");
        }
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = _context.Users.FirstOrDefault(x => x.Email == email);

        return user == null ? NotFound() : Ok(ToProfile(user));
    }

    private async Task<AuthResponseDto> BuildGoogleAuthResponse(
        GoogleJsonWebSignature.Payload payload,
        string defaultRole)
    {
        if (!payload.EmailVerified || string.IsNullOrWhiteSpace(payload.Email))
        {
            throw new InvalidOperationException("Google email is not verified.");
        }

        var user = _context.Users.FirstOrDefault(x =>
            x.GoogleSubjectId == payload.Subject ||
            x.Email == payload.Email);

        if (user == null)
        {
            user = new User
            {
                Id = Guid.NewGuid(),
                Name = payload.Name ?? payload.Email,
                Email = payload.Email,
                PasswordHash = string.Empty,
                Role = defaultRole,
                CreatedAt = DateTime.UtcNow,
                GoogleSubjectId = payload.Subject,
                AuthProvider = "Google"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            var changed = false;

            if (string.IsNullOrWhiteSpace(user.GoogleSubjectId))
            {
                user.GoogleSubjectId = payload.Subject;
                changed = true;
            }

            if (!string.Equals(user.AuthProvider, "Google", StringComparison.OrdinalIgnoreCase))
            {
                user.AuthProvider = "Google";
                changed = true;
            }

            if (!string.Equals(user.Name, payload.Name, StringComparison.Ordinal) &&
                !string.IsNullOrWhiteSpace(payload.Name))
            {
                user.Name = payload.Name;
                changed = true;
            }

            if (changed)
            {
                await _context.SaveChangesAsync();
            }
        }

        return new AuthResponseDto
        {
            Token = _tokenService.CreateToken(user),
            User = ToProfile(user)
        };
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

    private sealed class GoogleTokenResponse
    {
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; } = string.Empty;
    }
}
