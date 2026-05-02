namespace IdentityService.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UserProfileDto? User { get; set; }
    public bool RequiresOtp { get; set; } = false;
    public string Email { get; set; } = string.Empty;
}
