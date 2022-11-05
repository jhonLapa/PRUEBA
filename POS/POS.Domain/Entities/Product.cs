namespace POS.Domain.Entities
{
    public partial class Product : BaseEntity
    {
        public Product()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        // public int ProductId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public decimal SellPrice { get; set; }
        public int CategoryId { get; set; }


        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
