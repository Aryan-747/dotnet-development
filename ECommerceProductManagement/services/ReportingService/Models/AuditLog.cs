namespace ReportingService.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public string Action { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
