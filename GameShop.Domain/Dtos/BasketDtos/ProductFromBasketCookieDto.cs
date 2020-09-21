namespace GameShop.Domain.Dtos.BasketDtos
{
    public class ProductFromBasketCookieDto
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int StockQty { get; set; }
    }
}