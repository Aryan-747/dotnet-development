namespace ProductWorkflowService.DTOs;

public class InventoryDto
{
    public int Quantity { get; set; }
    public string AvailabilityMessage { get; set; } = string.Empty;
}
