using System.ComponentModel.DataAnnotations;

namespace POS.Infrastructure.Commons.Sale.Request
{
    public class sp_CrearVentaEntityRequest
    {
        [Key]
        public int? UserId { get; set; }
        public string? Client { get; set; }
        public decimal? Total { get; set; }
        public string? product { get; set; }

    }
}
