using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportingService.Data;

namespace ReportingService.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportingDbContext _context;

        public ReportsController(ReportingDbContext context)
        {
            _context = context;
        }

        [HttpGet("audit/{productId}")]
        public IActionResult GetAudit(Guid productId)
        {
            var logs = _context.AuditLogs
                .Where(x => x.ProductId == productId)
                .ToList();

            return Ok(logs);
        }
    }
}
