using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Sale.Request;
using POS.Infrastructure.Commons.Sale.Response;
using System.Reflection;

namespace POS.Infrastructure.Persistences.Contexts
{
    public partial class POSContext : DbContext
    {
        public POSContext()
        {
        }

        public POSContext(DbContextOptions<POSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<SaleDetail> SaleDetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        #region DbSets  Procedure
        public virtual DbSet<sp_CrearVentaEntityRequest> Sp_CrearVentaEntityResponses { get; set; }
        public virtual DbSet<sp_ListaComprasUsersEntityResponse> Sp_ListaComprasUsersEntityResponses { get; set; }
        public virtual DbSet<EliminarSaleEntityRequest> EliminarSaleRequests { get; set; }
        public virtual DbSet<sp_EliminarSaleDetalleEntityRequest> Sp_EliminarSaleDetalleEntityRequests { get; set; }
        public virtual DbSet<sp_EditarVentaEntityRequest> Sp_EditarVentaEntityRequests { get; set; }

        
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "ModernSpanish_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
