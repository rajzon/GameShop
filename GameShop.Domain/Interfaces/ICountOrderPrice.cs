using System.Collections.Generic;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.StockDto;
using GameShop.Domain.Interfaces;

namespace GameShop.Application.Interfaces
{
    public interface ICountOrderPrice
    {
        decimal Do<T>(List<T> productsForBasketDto)  where T: IOrderLogisticInfo;
    }
}