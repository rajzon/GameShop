using System.Collections.Generic;

namespace GameShop.Domain.Dtos.BasketDtos
{
    public class BasketDto
    {
        public decimal BasketPrice { get; set; }
        public List<ProductForBasketDto> BasketProducts { get; set; }
    }
}