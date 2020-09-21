using System.Collections.Generic;
using System.Linq;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GameShop.Application.Basket
{
    public class AddProductToBasket : IAddProductToBasket
    {

        public AddProductToBasket()
        {

        }

        public void Do(ISession session, Stock stockToAdd, int stockQty)
        {
            var basketJson = session.GetString("Basket");
            var basket = new List<ProductFromBasketCookieDto>();

            if (!string.IsNullOrEmpty(basketJson))
            {
               basket =  JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);
            }
            if (basket.Any(x => x.StockId == stockToAdd.Id))
            {
                basket.FirstOrDefault(x => x.StockId == stockToAdd.Id).StockQty += stockQty;
            } 
            else 
            {            
                var basketInfoForProduct = new ProductFromBasketCookieDto
                {
                    StockId = stockToAdd.Id,
                    ProductId = stockToAdd.ProductId,
                    StockQty = stockQty
                };
                basket.Add(basketInfoForProduct);

            }

            basketJson = JsonConvert.SerializeObject(basket);
            session.SetString("Basket",basketJson);
        }
    }
}