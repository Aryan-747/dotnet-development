namespace ProductWorkflowService.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
