using System.Collections.Generic;
using GameShop.Domain.Dtos.StockDto;

namespace GameShop.Domain.Dtos.BasketDtos
{
    public class BasketDto
    {
        public decimal BasketPrice { get; set; }
        public List<StockWithProductForBasketDto> BasketProducts { get; set; }
    }
}