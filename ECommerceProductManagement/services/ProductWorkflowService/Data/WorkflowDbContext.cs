using Microsoft.EntityFrameworkCore;
using ProductWorkflowService.Models;

namespace ProductWorkflowService.Data
{
    public class WorkflowDbContext : DbContext
    {
        public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options)
            : base(options) { }

        public DbSet<Price> Prices { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Approval> Approvals { get; set; }
    }
}
