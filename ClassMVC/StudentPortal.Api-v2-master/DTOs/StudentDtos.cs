using System.ComponentModel.DataAnnotations;
namespace StudentPortal.Api.DTOs;
public class StudentDto
{
    public int StudentId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateOnly JoinDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class StudentCreateDto
{
    [Required, StringLength(120)] public string FullName { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(180)] public string Email { get; set; } = string.Empty;
    [StringLength(30)] public string? Phone { get; set; }
    [Required, StringLength(20)] public string Status { get; set; } = "Active";
    [Required] public DateOnly JoinDate { get; set; }
}
public class StudentUpdateDto : StudentCreateDto { [Required] public int StudentId { get; set; } }