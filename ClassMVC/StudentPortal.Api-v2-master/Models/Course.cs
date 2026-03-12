namespace StudentPortal.Api.Models;
public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationDays { get; set; }
    public decimal Fee { get; set; }
    public string Level { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}