namespace ReportingService.DTOs;

public class AuditCreateDto
{
    public Guid ProductId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}
