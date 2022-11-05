using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace POS.Domain.Models
{
    public partial class POSPRUEBAContext : DbContext
    {
        public POSPRUEBAContext()
        {
        }

        public POSPRUEBAContext(DbContextOptions<POSPRUEBAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<SaleDetail> SaleDetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=POSPRUEBA;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SellPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Catego__2A4B4B5E");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.Client)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Sales__UserId__300424B4");
            });

            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SaleDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__SaleDetai__Produ__32E0915F");

                entity.HasOne(d => d.Sale)
                    .WithMany(p => p.SaleDetails)
                    .HasForeignKey(d => d.SaleId)
                    .HasConstraintName("FK__SaleDetai__SaleI__33D4B598");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.AuditDeleteDate).HasColumnType("datetime");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
