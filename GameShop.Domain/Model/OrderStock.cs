namespace GameShop.Domain.Model
{
    public class OrderStock
    {
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}