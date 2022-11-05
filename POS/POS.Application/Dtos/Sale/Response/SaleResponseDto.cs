using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Sale.Response
{
    public class SaleResponseDto
    {
        public int SaleId { get; set; }
        public string? Client { get; set; }
        public int? UserId { get; set; }
        public decimal? Total { get; set; }
        public string? StateSale { get; set; }

        
    }
}
