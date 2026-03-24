namespace ProductWorkflowService.Models
{
    public class Price
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public decimal MRP { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
