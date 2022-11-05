using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistences.Contexts.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("SaleId");

            builder.Property(e => e.Client)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Sales)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Sales__UserId__300424B4");
        }
    }
}
