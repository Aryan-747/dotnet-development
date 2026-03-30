namespace IdentityService.Models;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string? GoogleSubjectId { get; set; }
    public string AuthProvider { get; set; } = "Local";
}
