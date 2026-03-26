using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportingService.Data;
using ReportingService.DTOs;
using ReportingService.Models;
using System.Security.Claims;
using System.Text;

namespace ReportingService.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportingDbContext _context;

        public ReportsController(ReportingDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard")]
        public IActionResult GetDashboard()
        {
            var logs = _context.AuditLogs.ToList();
            var today = DateTime.UtcNow.Date;

            return Ok(new
            {
                totalActivities = logs.Count,
                auditsToday = logs.Count(x => x.CreatedAt.Date == today),
                pendingAlerts = logs.Count(x => x.Action.Contains("Rejected") || x.Action.Contains("Pending")),
                recentActivities = logs.OrderByDescending(x => x.CreatedAt).Take(5).ToList()
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("alerts")]
        public IActionResult GetAlerts()
        {
            return Ok(_context.AuditLogs
                .Where(x => x.Action.Contains("Rejected") || x.Action.Contains("Pending"))
                .OrderByDescending(x => x.CreatedAt)
                .Take(10)
                .ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("audit/{productId:guid}")]
        public IActionResult GetAudit(Guid productId)
        {
            var logs = _context.AuditLogs
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return Ok(logs);
        }

        [Authorize]
        [HttpPost("audit")]
        public async Task<IActionResult> CreateAudit(AuditCreateDto dto)
        {
            var log = new AuditLog
            {
                Id = Guid.NewGuid(),
                ProductId = dto.ProductId,
                Action = dto.Action,
                EntityName = dto.EntityName,
                Details = dto.Details,
                ActorEmail = User.FindFirstValue(ClaimTypes.Email) ?? "unknown",
                CreatedAt = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();

            return Ok(log);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("export/audit/{productId:guid}")]
        public IActionResult ExportAudit(Guid productId)
        {
            var logs = _context.AuditLogs
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            var builder = new StringBuilder();
            builder.AppendLine("CreatedAt,ActorEmail,EntityName,Action,Details");

            foreach (var log in logs)
            {
                builder.AppendLine($"{log.CreatedAt:O},{log.ActorEmail},{log.EntityName},{log.Action},\"{log.Details.Replace("\"", "\"\"")}\"");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", $"audit-{productId}.csv");
        }
    }
}
