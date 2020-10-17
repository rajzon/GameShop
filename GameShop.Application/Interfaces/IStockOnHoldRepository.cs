using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.StockDto;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace GameShop.Application.Interfaces
{
    public interface IStockOnHoldRepository : IBaseRepository<StockOnHold>
    {
        Task<List<StockOnHoldWithProductForCountOrderPriceDto>> GetStockOnHoldWithProductForCharge(ISession session, List<ProductFromBasketCookieDto> basketProductsCookie);
    }
}