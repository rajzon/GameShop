using System.Collections.Generic;

namespace GameShop.Domain.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        //TO DO: implement post code validation
        public string PostCode { get; set; }
        public string City { get; set; }

        public ICollection<OrderStock> OrderStocks { get; set; }

    }
}