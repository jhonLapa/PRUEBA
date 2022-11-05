using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Sale.Response
{
    public class SaleDetailByIdResponseDto
    {
        public int SaleDetailId { get; set; }
        public int? Amount { get; set; }
        public int? SaleId { get; set; }
        public int? ProductId { get; set; }
        public double? PriceTotalProduct { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public decimal SellPrice { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }

    }
}
