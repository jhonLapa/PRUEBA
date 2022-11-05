using System.ComponentModel.DataAnnotations;

namespace POS.Infrastructure.Commons.Sale.Request
{
    public class sp_EditarVentaEntityRequest
    {
        [Key]
        public int? SaleId { get; set; }
        public int? UserId { get; set; }
        public string? Client { get; set; }
        public decimal? Total { get; set; }
        public string? product { get; set; }
    }
}
