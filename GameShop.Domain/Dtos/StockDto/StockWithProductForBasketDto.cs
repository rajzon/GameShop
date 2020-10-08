using GameShop.Domain.Interfaces;

namespace GameShop.Domain.Dtos.StockDto
{
    public class StockWithProductForBasketDto : IOrderLogisticInfo
    {
        public int ProductId { get; set; }
        public int StockId { get; set; }
        public int StockQty { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}