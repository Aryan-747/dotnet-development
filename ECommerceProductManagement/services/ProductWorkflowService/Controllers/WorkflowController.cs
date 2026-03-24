using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWorkflowService.Data;
using ProductWorkflowService.Models;

namespace ProductWorkflowService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/workflow")]
    public class WorkflowController : ControllerBase
    {
        private readonly WorkflowDbContext _context;

        public WorkflowController(WorkflowDbContext context)
        {
            _context = context;
        }

        [HttpPut("pricing/{productId}")]
        public async Task<IActionResult> SavePricing(Guid productId, Price price)
        {
            if (price.SellingPrice > price.MRP)
                return BadRequest("Invalid price");

            price.ProductId = productId;

            _context.Prices.Add(price);
            await _context.SaveChangesAsync();

            return Ok(price);
        }

        [HttpPut("inventory/{productId}")]
        public async Task<IActionResult> SaveInventory(Guid productId, Inventory inventory)
        {
            inventory.ProductId = productId;

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return Ok(inventory);
        }

        [HttpPost("submit/{productId}")]
        public async Task<IActionResult> Submit(Guid productId)
        {
            var approval = new Approval
            {
                ProductId = productId,
                Status = "Pending"
            };

            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();

            return Ok("Submitted for review");
        }
    }
}
