using System.Collections.Generic;

namespace GameShop.Domain.Model
{
    public class Stock
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<OrderStock> OrderStocks { get; set; }

    }
}