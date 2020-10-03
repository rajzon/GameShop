namespace GameShop.Domain.Dtos.StockDto
{
    public class StockWithProductForCountOrderPrice
    {
        public int ProductId { get; set; }
        public int StockId { get; set; }
        public int StockQty { get; set; }
        public decimal Price { get; set; }
        
    }
}