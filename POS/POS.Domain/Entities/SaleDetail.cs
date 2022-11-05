using System;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public partial class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public int? Amount { get; set; }
        public int? SaleId { get; set; }
        public int? ProductId { get; set; }
        public double? PriceTotalProduct { get; set; }
        public int AuditCreateUser { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public int? AuditUpdateUser { get; set; }
        public DateTime? AuditUpdateDate { get; set; }
        public int? AuditDeleteUser { get; set; }
        public DateTime? AuditDeleteDate { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Sale? Sale { get; set; }
    }
}
