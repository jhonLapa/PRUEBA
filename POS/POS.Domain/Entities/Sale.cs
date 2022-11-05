using System;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public partial class Sale : BaseEntity
    {
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        //public int SaleId { get; set; }
        public string? Client { get; set; }
        public int? UserId { get; set; }
        public decimal? Total { get; set; }


        public virtual User? User { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
