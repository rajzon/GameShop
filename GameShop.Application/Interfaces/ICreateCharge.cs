using System.Collections.Generic;
using GameShop.Domain.Dtos.BasketDtos;

namespace GameShop.Application.Interfaces
{
    public interface ICreateCharge
    {
         BasketForPaymentDto Do(string stripeToken, decimal basketPrice, List<ProductFromBasketCookieDto> basketProductsCookie);
    }
}