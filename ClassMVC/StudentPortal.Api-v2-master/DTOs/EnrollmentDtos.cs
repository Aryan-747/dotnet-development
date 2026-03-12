using System.ComponentModel.DataAnnotations;
namespace StudentPortal.Api.DTOs;
public class EnrollmentDto
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public DateOnly EnrollDate { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public decimal PaidAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class EnrollmentCreateDto
{
    [Required] public int StudentId { get; set; }
    [Required] public int CourseId { get; set; }
    [Required] public DateOnly EnrollDate { get; set; }
    [Required, StringLength(20)] public string PaymentStatus { get; set; } = "Pending";
    [Range(0,999999)] public decimal PaidAmount { get; set; }
}
public class EnrollmentUpdateDto : EnrollmentCreateDto { [Required] public int EnrollmentId { get; set; } }