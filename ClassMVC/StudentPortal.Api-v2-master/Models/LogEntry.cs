namespace StudentPortal.Api.Models;
public class LogEntry
{
    public int StudentId { get; set; }
    public int LogId { get; set; }
    public string? Info { get; set; }
    public Student? Student { get; set; }
}