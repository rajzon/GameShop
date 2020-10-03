using System.Collections.Generic;

namespace GameShop.Domain.Dtos.BasketDtos
{
    public class BasketForPaymentDto
    {
        public decimal BasketPrice { get; set; }
        public string StripeRef { get; set; }
        public List<ProductFromBasketCookieDto> BasketProducts { get; set; }
        
    }
}