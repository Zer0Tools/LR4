using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LR4.Models;

namespace LR4;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryHasGood> CategoryHasGoods { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleHasGood> SaleHasGoods { get; set; }

    public virtual DbSet<SaleHistory> SaleHistories { get; set; }

    public virtual DbSet<Source> Sources { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=albert;Password=ALBERT;Timeout=100;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category", "store");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CategoryHasGood>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("category_has_good", "store");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.GoodId).HasColumnName("good_id");

            entity.HasOne(d => d.Category).WithMany()
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("category_has_good_category_id_fkey");

            entity.HasOne(d => d.Good).WithMany()
                .HasForeignKey(d => d.GoodId)
                .HasConstraintName("category_has_good_good_id_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client", "store");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(45)
                .HasColumnName("code");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.SourceId).HasColumnName("source_id");

            entity.HasOne(d => d.Source).WithMany(p => p.Clients)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("client_source_id_fkey");
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("good_pkey");

            entity.ToTable("good", "store");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(16, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sale_pkey");

            entity.ToTable("sale", "store");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DtCreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dt_modified");
            entity.Property(e => e.Number)
                .HasMaxLength(255)
                .HasColumnName("number");
            entity.Property(e => e.SaleSum)
                .HasPrecision(18, 2)
                .HasColumnName("sale_sum");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("sale_client_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("sale_status_id_fkey");
        });

        modelBuilder.Entity<SaleHasGood>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sale_has_good", "store");

            entity.Property(e => e.GoodId).HasColumnName("good_id");
            entity.Property(e => e.SaleId).HasColumnName("sale_id");

            entity.HasOne(d => d.Good).WithMany()
                .HasForeignKey(d => d.GoodId)
                .HasConstraintName("sale_has_good_good_id_fkey");

            entity.HasOne(d => d.Sale).WithMany()
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("sale_has_good_sale_id_fkey");
        });

        modelBuilder.Entity<SaleHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sale_history_pkey");

            entity.ToTable("sale_history", "store");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ActiveFrom)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("active_from");
            entity.Property(e => e.ActiveTo)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("active_to");
            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.SaleSum)
                .HasPrecision(18, 2)
                .HasColumnName("sale_sum");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleHistories)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("sale_history_sale_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.SaleHistories)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("sale_history_status_id_fkey");
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("source_pkey");

            entity.ToTable("source", "store");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status", "store");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
