using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentPortal.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=ARYAN\\SQLEXPRESS;Database=StudentDbCodeFirst;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}