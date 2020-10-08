using System.Collections.Generic;
using System.Linq;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GameShop.Application.Basket
{
    public class AddStockToBasket : IAddStockToBasket
    {

        public AddStockToBasket()
        {

        }


        public void Do(ISession session, StockOnHold stockToUpdate)
        {
            var basketJson = session.GetString("Basket");
            var basket = new List<ProductFromBasketCookieDto>();

            if (!string.IsNullOrEmpty(basketJson))
            {
               basket =  JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);
            }
            if (basket.Any(x => x.StockId == stockToUpdate.StockId))
            {
                basket.FirstOrDefault(x => x.StockId == stockToUpdate.StockId).StockQty = stockToUpdate.StockQty;
            } 
            else 
            {            
                var basketInfoForProduct = new ProductFromBasketCookieDto
                {
                    StockId = stockToUpdate.StockId,
                    //In The Future Remove That ProductId
                    ProductId = stockToUpdate.ProductId,
                    StockQty = stockToUpdate.StockQty
                };
                basket.Add(basketInfoForProduct);

            }

            basketJson = JsonConvert.SerializeObject(basket);
            session.SetString("Basket",basketJson);
        }
    }
}