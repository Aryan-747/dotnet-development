namespace StudentPortal.Api.Models;
public class Enrollment
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateOnly EnrollDate { get; set; }
    public string PaymentStatus { get; set; } = "Pending";
    public decimal PaidAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Student? Student { get; set; }
    public Course? Course { get; set; }
}