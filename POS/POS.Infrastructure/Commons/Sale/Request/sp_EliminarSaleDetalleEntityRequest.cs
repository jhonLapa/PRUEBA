using System.ComponentModel.DataAnnotations;

namespace POS.Infrastructure.Commons.Sale.Request
{
    public class sp_EliminarSaleDetalleEntityRequest
    {
        [Key]
        public int SaleId { get; set; }
        public string Product { get; set; }
    }
}
