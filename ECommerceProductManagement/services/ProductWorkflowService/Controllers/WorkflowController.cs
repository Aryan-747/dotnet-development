using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWorkflowService.Data;
using ProductWorkflowService.DTOs;
using ProductWorkflowService.Models;
using System.Security.Claims;

namespace ProductWorkflowService.Controllers
{
    [ApiController]
    [Route("api/workflow")]
    public class WorkflowController : ControllerBase
    {
        private readonly WorkflowDbContext _context;

        public WorkflowController(WorkflowDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("product/{productId:guid}")]
        public IActionResult GetState(Guid productId)
        {
            var price = _context.Prices
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.UpdatedAt)
                .FirstOrDefault();

            var inventory = _context.Inventories
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.UpdatedAt)
                .FirstOrDefault();

            var approval = _context.Approvals
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.UpdatedAt)
                .FirstOrDefault();

            return Ok(new
            {
                productId,
                price,
                inventory,
                approval
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("queue")]
        public IActionResult GetQueue()
        {
            return Ok(_context.Approvals.OrderByDescending(x => x.UpdatedAt).ToList());
        }

        [Authorize(Roles = "Admin,ProductManager")]
        [HttpPut("pricing/{productId:guid}")]
        public async Task<IActionResult> SavePricing(Guid productId, PriceDto dto)
        {
            if (dto.SellingPrice > dto.Mrp)
            {
                return BadRequest("Invalid price");
            }

            var price = _context.Prices.FirstOrDefault(x => x.ProductId == productId)
                ?? new Price { Id = Guid.NewGuid(), ProductId = productId };

            price.MRP = dto.Mrp;
            price.SellingPrice = dto.SellingPrice;
            price.UpdatedAt = DateTime.UtcNow;

            _context.Prices.Update(price);
            await _context.SaveChangesAsync();

            return Ok(price);
        }

        [Authorize(Roles = "Admin,ProductManager")]
        [HttpPut("inventory/{productId:guid}")]
        public async Task<IActionResult> SaveInventory(Guid productId, InventoryDto dto)
        {
            var inventory = _context.Inventories.FirstOrDefault(x => x.ProductId == productId)
                ?? new Inventory { Id = Guid.NewGuid(), ProductId = productId };

            inventory.Quantity = dto.Quantity;
            inventory.AvailabilityMessage = string.IsNullOrWhiteSpace(dto.AvailabilityMessage)
                ? (dto.Quantity > 0 ? "In Stock" : "Out of Stock")
                : dto.AvailabilityMessage;
            inventory.UpdatedAt = DateTime.UtcNow;

            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();

            return Ok(inventory);
        }

        [Authorize(Roles = "Admin,ProductManager")]
        [HttpPost("submit/{productId:guid}")]
        public async Task<IActionResult> Submit(Guid productId)
        {
            var approval = _context.Approvals.FirstOrDefault(x => x.ProductId == productId)
                ?? new Approval { Id = Guid.NewGuid(), ProductId = productId };

            approval.Status = "Pending";
            approval.Remarks = "Submitted for admin review";
            approval.RequestedBy = User.FindFirstValue(ClaimTypes.Email) ?? "unknown";
            approval.ReviewedBy = string.Empty;
            approval.UpdatedAt = DateTime.UtcNow;

            _context.Approvals.Update(approval);
            await _context.SaveChangesAsync();

            return Ok(approval);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("approve/{productId:guid}")]
        public async Task<IActionResult> Approve(Guid productId, ApprovalActionDto dto)
        {
            var approval = _context.Approvals.FirstOrDefault(x => x.ProductId == productId);

            if (approval == null)
            {
                return NotFound();
            }

            approval.Status = "Approved";
            approval.Remarks = dto.Remarks;
            approval.ReviewedBy = User.FindFirstValue(ClaimTypes.Email) ?? "unknown";
            approval.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(approval);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("reject/{productId:guid}")]
        public async Task<IActionResult> Reject(Guid productId, ApprovalActionDto dto)
        {
            var approval = _context.Approvals.FirstOrDefault(x => x.ProductId == productId);

            if (approval == null)
            {
                return NotFound();
            }

            approval.Status = "Rejected";
            approval.Remarks = dto.Remarks;
            approval.ReviewedBy = User.FindFirstValue(ClaimTypes.Email) ?? "unknown";
            approval.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(approval);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("publish/{productId:guid}")]
        public async Task<IActionResult> Publish(Guid productId)
        {
            var approval = _context.Approvals.FirstOrDefault(x => x.ProductId == productId);

            if (approval == null || approval.Status != "Approved")
            {
                return BadRequest("Product must be approved before publishing.");
            }

            approval.Status = "Published";
            approval.Remarks = "Published to storefront preview";
            approval.ReviewedBy = User.FindFirstValue(ClaimTypes.Email) ?? "unknown";
            approval.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(approval);
        }
    }
}
