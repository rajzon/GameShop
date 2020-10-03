using System.Collections.Generic;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.StockDto;

namespace GameShop.Application.Interfaces
{
    public interface ICountOrderPrice
    {
        decimal Do(List<ProductForBasketDto> productsForBasketDto);
    }
}