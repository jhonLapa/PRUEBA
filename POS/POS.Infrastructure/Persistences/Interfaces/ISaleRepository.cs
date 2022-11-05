using POS.Domain.Entities;
using POS.Infrastructure.Commons.Sale.Request;
using POS.Infrastructure.Commons.Sale.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<bool> RegisterSaleProcedure(sp_CrearVentaEntityRequest sale);
        Task<bool> RemoveSaleProcedure(EliminarSaleEntityRequest sale);
        Task<bool> RemoveSaleDetailProcedure(sp_EliminarSaleDetalleEntityRequest sale);
        Task<bool> UpdateSaleProcedure(sp_EditarVentaEntityRequest sale);
        Task<IEnumerable<sp_ListaComprasUsersEntityResponse>> ReporteSaleProcedure(int userId);
        Task<IEnumerable<SaleDetail>> SaleDetailById(int saleId);

    }
}
