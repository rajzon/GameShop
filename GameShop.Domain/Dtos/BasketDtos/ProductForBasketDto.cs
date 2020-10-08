using GameShop.Domain.Interfaces;

namespace GameShop.Domain.Dtos.BasketDtos
{
    public class ProductForBasketDto : IOrderLogisticInfo
    {
        public int ProductId { get; set; }
        public int StockId { get; set; }
        public int StockQty { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}