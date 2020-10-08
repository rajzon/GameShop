using GameShop.Domain.Interfaces;

namespace GameShop.Domain.Dtos.StockDto
{
    public class StockWithProductForCountOrderPrice : IOrderLogisticInfo
    {
        public int ProductId { get; set; }
        public int StockId { get; set; }
        public int StockQty { get; set; }
        public decimal Price { get; set; }
        
    }
}