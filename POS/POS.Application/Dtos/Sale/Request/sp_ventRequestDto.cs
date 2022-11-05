using System.ComponentModel.DataAnnotations;
namespace POS.Application.Dtos.Sale.Request
{
    public class sp_ventRequestDto
    {
        [Key]
        public int? UserId { get; set; }
        public string? Client { get; set; }
        public decimal? Total { get; set; }
        public string? product { get; set; }
    }
}
