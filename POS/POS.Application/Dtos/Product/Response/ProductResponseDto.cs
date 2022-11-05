namespace POS.Application.Dtos.Product.Response
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public decimal SellPrice { get; set; }
        public int CategoryId { get; set; }
        public string? StateProduct { get; set; }

    }
}
