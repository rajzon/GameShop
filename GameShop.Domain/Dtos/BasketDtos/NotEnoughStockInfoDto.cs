namespace GameShop.Domain.Dtos.BasketDtos
{
    public class NotEnoughStockInfoDto
    {
        public int StockId { get; set; }
        public int AvailableStockQty { get; set; }
        public string ProductName { get; set; }
    }
}