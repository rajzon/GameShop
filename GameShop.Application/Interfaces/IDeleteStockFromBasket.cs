using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Domain.Dtos.BasketDtos;
using Microsoft.AspNetCore.Http;

namespace GameShop.Application.Interfaces
{
    public interface IDeleteStockFromBasket
    {
         Task<bool> Do(ISession session, List<ProductFromBasketCookieDto> basketCookie, int stockIdToDelete);
    }
}