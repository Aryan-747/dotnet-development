namespace ProductWorkflowService.Models
{
    public class Price
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        [Microsoft.EntityFrameworkCore.Precision(18, 2)]
        public decimal MRP { get; set; }
        
        [Microsoft.EntityFrameworkCore.Precision(18, 2)]
        public decimal SellingPrice { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
