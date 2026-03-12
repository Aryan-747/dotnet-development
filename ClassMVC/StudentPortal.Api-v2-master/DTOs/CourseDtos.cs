using System.ComponentModel.DataAnnotations;
namespace StudentPortal.Api.DTOs;
public class CourseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationDays { get; set; }
    public decimal Fee { get; set; }
    public string Level { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class CourseCreateDto
{
    [Required, StringLength(150)] public string Title { get; set; } = string.Empty;
    [Range(1,3650)] public int DurationDays { get; set; }
    [Range(0,999999)] public decimal Fee { get; set; }
    [Required, StringLength(30)] public string Level { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
public class CourseUpdateDto : CourseCreateDto { [Required] public int CourseId { get; set; } }