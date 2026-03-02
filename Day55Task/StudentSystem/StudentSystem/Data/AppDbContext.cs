using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;

namespace StudentSystem.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Courses> Courses { get; set; }

    public virtual DbSet<Employees> Employees { get; set; }

    public virtual DbSet<Enrollments> Enrollments { get; set; }

    public virtual DbSet<Student> Student { get; set; }

    public virtual DbSet<Students> Students { get; set; }

    public virtual DbSet<TEMPORARY> TEMPORARY { get; set; }

    public virtual DbSet<UserProfiles> UserProfiles { get; set; }

    public virtual DbSet<brands> brands { get; set; }

    public virtual DbSet<categories> categories { get; set; }

    public virtual DbSet<customers> customers { get; set; }

    public virtual DbSet<order_items> order_items { get; set; }

    public virtual DbSet<orders> orders { get; set; }

    public virtual DbSet<products> products { get; set; }

    public virtual DbSet<staffs> staffs { get; set; }

    public virtual DbSet<stocks> stocks { get; set; }

    public virtual DbSet<stores> stores { get; set; }

    public virtual DbSet<tblLog> tblLog { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ARYAN\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Courses>(entity =>
        {
            entity.HasKey(e => e.CourseId);

            entity.HasIndex(e => e.Title, "IX_Courses_Title");

            entity.Property(e => e.Fee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Level).HasMaxLength(30);
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<Employees>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07474018FB");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Enrollments>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId);

            entity.HasIndex(e => e.CourseId, "IX_Enrollments_CourseId");

            entity.HasIndex(e => e.StudentId, "IX_Enrollments_StudentId");

            entity.HasIndex(e => new { e.StudentId, e.CourseId }, "UQ_Enrollments_StudentCourse").IsUnique();

            entity.Property(e => e.PaidAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentID).HasName("PK__Student__32C52A79BF9439A8");

            entity.Property(e => e.StudentID).ValueGeneratedNever();
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Students>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.HasIndex(e => e.Email, "UX_Students_Email").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(180);
            entity.Property(e => e.FullName).HasMaxLength(120);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<TEMPORARY>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserProfiles>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__UserProf__1788CCACDFB76C99");

            entity.Property(e => e.UserID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<brands>(entity =>
        {
            entity.HasKey(e => e.brand_id).HasName("PK__brands__5E5A8E271C1845DB");

            entity.ToTable("brands", "production");

            entity.Property(e => e.brand_name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<categories>(entity =>
        {
            entity.HasKey(e => e.category_id).HasName("PK__categori__D54EE9B474811C23");

            entity.ToTable("categories", "production");

            entity.Property(e => e.category_name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<customers>(entity =>
        {
            entity.HasKey(e => e.customer_id).HasName("PK__customer__CD65CB8559038FE1");

            entity.ToTable("customers", "sales");

            entity.Property(e => e.city)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.first_name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.last_name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.phone)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.state)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.street)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.zip_code)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<order_items>(entity =>
        {
            entity.HasKey(e => new { e.order_id, e.item_id }).HasName("PK__order_it__837942D4E6DE9019");

            entity.ToTable("order_items", "sales");

            entity.Property(e => e.discount).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.list_price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.order).WithMany(p => p.order_items)
                .HasForeignKey(d => d.order_id)
                .HasConstraintName("FK__order_ite__order__1352D76D");

            entity.HasOne(d => d.product).WithMany(p => p.order_items)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("FK__order_ite__produ__1446FBA6");
        });

        modelBuilder.Entity<orders>(entity =>
        {
            entity.HasKey(e => e.order_id).HasName("PK__orders__46596229FA1988F1");

            entity.ToTable("orders", "sales");

            entity.HasOne(d => d.customer).WithMany(p => p.orders)
                .HasForeignKey(d => d.customer_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__orders__customer__0D99FE17");

            entity.HasOne(d => d.staff).WithMany(p => p.orders)
                .HasForeignKey(d => d.staff_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__staff_id__0F824689");

            entity.HasOne(d => d.store).WithMany(p => p.orders)
                .HasForeignKey(d => d.store_id)
                .HasConstraintName("FK__orders__store_id__0E8E2250");
        });

        modelBuilder.Entity<products>(entity =>
        {
            entity.HasKey(e => e.product_id).HasName("PK__products__47027DF528621097");

            entity.ToTable("products", "production");

            entity.Property(e => e.list_price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.product_name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.brand).WithMany(p => p.products)
                .HasForeignKey(d => d.brand_id)
                .HasConstraintName("FK__products__brand___02284B6B");

            entity.HasOne(d => d.category).WithMany(p => p.products)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("FK__products__catego__01342732");
        });

        modelBuilder.Entity<staffs>(entity =>
        {
            entity.HasKey(e => e.staff_id).HasName("PK__staffs__1963DD9C45CAAE74");

            entity.ToTable("staffs", "sales");

            entity.HasIndex(e => e.email, "UQ__staffs__AB6E6164F19D38B0").IsUnique();

            entity.Property(e => e.email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.first_name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.last_name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.phone)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.manager).WithMany(p => p.Inversemanager)
                .HasForeignKey(d => d.manager_id)
                .HasConstraintName("FK__staffs__manager___0ABD916C");

            entity.HasOne(d => d.store).WithMany(p => p.staffs)
                .HasForeignKey(d => d.store_id)
                .HasConstraintName("FK__staffs__store_id__09C96D33");
        });

        modelBuilder.Entity<stocks>(entity =>
        {
            entity.HasKey(e => new { e.store_id, e.product_id }).HasName("PK__stocks__E68284D359D017FD");

            entity.ToTable("stocks", "production");

            entity.HasOne(d => d.product).WithMany(p => p.stocks)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("FK__stocks__product___18178C8A");

            entity.HasOne(d => d.store).WithMany(p => p.stocks)
                .HasForeignKey(d => d.store_id)
                .HasConstraintName("FK__stocks__store_id__17236851");
        });

        modelBuilder.Entity<stores>(entity =>
        {
            entity.HasKey(e => e.store_id).HasName("PK__stores__A2F2A30C8910C473");

            entity.ToTable("stores", "sales");

            entity.Property(e => e.city)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.phone)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.state)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.store_name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.street)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.zip_code)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.Property(e => e.LogId).ValueGeneratedNever();
            entity.Property(e => e.Info)
                .HasMaxLength(2000)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
