namespace ProductWorkflowService.Models
{
    public class Approval
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
