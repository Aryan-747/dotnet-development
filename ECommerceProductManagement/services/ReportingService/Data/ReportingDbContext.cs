using Microsoft.EntityFrameworkCore;
using ReportingService.Models;

namespace ReportingService.Data
{
    public class ReportingDbContext : DbContext
    {
        public ReportingDbContext(DbContextOptions<ReportingDbContext> options)
            : base(options) { }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
