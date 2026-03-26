namespace ProductWorkflowService.Models
{
    public class Approval
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public string Status { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public string RequestedBy { get; set; } = string.Empty;
        public string ReviewedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }
}
