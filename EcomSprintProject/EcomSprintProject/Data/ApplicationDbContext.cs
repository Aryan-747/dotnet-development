using EcomSprintProject.Models;
using EcomSprintProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomSprintProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}