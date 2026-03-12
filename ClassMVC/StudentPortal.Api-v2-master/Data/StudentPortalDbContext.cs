using Microsoft.EntityFrameworkCore;
using StudentPortal.Api.Models;

namespace StudentPortal.Api.Data;

public class StudentPortalDbContext : DbContext
{
    public StudentPortalDbContext(DbContextOptions<StudentPortalDbContext> options) : base(options) { }
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<LogEntry> TblLogs => Set<LogEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(e =>
        {
            e.HasKey(x => x.StudentId);
            e.Property(x => x.FullName).HasMaxLength(120).IsRequired();
            e.Property(x => x.Email).HasMaxLength(180).IsRequired();
            e.Property(x => x.Phone).HasMaxLength(30);
            e.Property(x => x.Status).HasMaxLength(20).HasDefaultValue("Active");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("sysdatetime()");
            e.HasIndex(x => x.Email).IsUnique().HasDatabaseName("UX_Students_Email");
        });

        modelBuilder.Entity<Course>(e =>
        {
            e.HasKey(x => x.CourseId);
            e.Property(x => x.Title).HasMaxLength(150).IsRequired();
            e.Property(x => x.Level).HasMaxLength(30).IsRequired();
            e.Property(x => x.Fee).HasColumnType("decimal(10,2)");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("sysdatetime()");
            e.HasIndex(x => x.Title).HasDatabaseName("IX_Courses_Title");
        });

        modelBuilder.Entity<Enrollment>(e =>
        {
            e.HasKey(x => x.EnrollmentId);
            e.Property(x => x.PaymentStatus).HasMaxLength(20).HasDefaultValue("Pending");
            e.Property(x => x.PaidAmount).HasColumnType("decimal(10,2)").HasDefaultValue(0);
            e.Property(x => x.CreatedAt).HasDefaultValueSql("sysdatetime()");
            e.HasIndex(x => x.StudentId).HasDatabaseName("IX_Enrollments_StudentId");
            e.HasIndex(x => x.CourseId).HasDatabaseName("IX_Enrollments_CourseId");
            e.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique().HasDatabaseName("UQ_Enrollments_StudentCourse");
            e.HasOne(x => x.Student).WithMany(x => x.Enrollments).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Course).WithMany(x => x.Enrollments).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LogEntry>(e =>
        {
            e.ToTable("tblLog");
            e.HasKey(x => x.LogId);
            e.Property(x => x.Info).HasMaxLength(2000).IsUnicode(false);
            e.HasOne(x => x.Student).WithMany(x => x.Logs).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}