using System;
using System.Collections.Generic;

namespace GameShop.Domain.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        //TO DO: implement post code validation
        public string PostCode { get; set; }
        public string City { get; set; }
        public string DeliveryType { get; set; }

        public Guid OrderRef { get; set; }
        public string StripeRef { get; set; }
        public decimal OrderPrice { get; set; }

        public ICollection<OrderStock> OrderStocks { get; set; }

    }
}