using System;
using System.Collections.Generic;
using DbFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DbFirstDemo.Data;

public partial class FoodieApp : DbContext
{
    public FoodieApp()
    {
    }

    public FoodieApp(DbContextOptions<FoodieApp> options)
        : base(options)
    {
    }

    public virtual DbSet<Employees> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ARYAN\\SQLEXPRESS;Database=TrainingDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employees>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11ACA7CCEF");

            entity.Property(e => e.Department).HasMaxLength(60);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("decimal(12, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
