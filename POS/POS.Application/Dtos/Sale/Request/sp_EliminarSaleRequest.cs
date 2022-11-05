using System.ComponentModel.DataAnnotations;
namespace POS.Application.Dtos.Sale.Request
{
    public class sp_EliminarSaleRequest
    {
        [Key]
        public int SaleId { get; set; }
        public string Product { get; set; }
    }
}
