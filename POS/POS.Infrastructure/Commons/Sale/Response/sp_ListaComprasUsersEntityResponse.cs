using System.ComponentModel.DataAnnotations;


namespace POS.Infrastructure.Commons.Sale.Response
{
    public class sp_ListaComprasUsersEntityResponse
    {
        [Key]

        public int SaleId { get; set; }
        public string? Client { get; set; }
        public int? UserId { get; set; }
        public decimal? Total { get; set; }
    }
}
