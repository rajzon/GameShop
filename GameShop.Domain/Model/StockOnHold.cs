using System;

namespace GameShop.Domain.Model
{
    public class StockOnHold
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int StockQty { get; set; }
        public DateTime ExpireTime { get; set; }
        public string SessionId { get; set; }
        public int ProductId { get; set; }
        
    }
}