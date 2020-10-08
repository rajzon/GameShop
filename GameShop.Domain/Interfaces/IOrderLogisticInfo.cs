namespace GameShop.Domain.Interfaces
{
    public interface IOrderLogisticInfo
    {
        int ProductId { get; set; }
        int StockId { get; set; }
        int StockQty { get; set; }
        decimal Price { get; set; }
    }
}