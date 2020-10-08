using System.Collections.Generic;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.StockDto;
using GameShop.Domain.Interfaces;

namespace GameShop.Domain.Logic
{
    public class CountOrderPrice : ICountOrderPrice
    {

        //ToDO: Create another Dto for method parameter that is more special for that action for example: i do not need Product Name
        public decimal Do<T>(List<T> productsForBasketDto) where T: IOrderLogisticInfo
        {
            //TODO: Call method For checking if productFromCookie that was passed exists in Db, return - 1 and in controllers make IF statement that returns BadRequest

            decimal basketPrice = 0;
            foreach (var product in productsForBasketDto)
            {
                basketPrice += product.Price * product.StockQty;
            }

            return basketPrice;
        }
    }
}