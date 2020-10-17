using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.StockDto;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Repositories
{
    public class StockOnHoldRepository : BaseRepository<StockOnHold>, IStockOnHoldRepository
    {
        public StockOnHoldRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
        }

        //Consider make if that check StockQty from StockOnHold is same as ProductFromBasketCookieDto.StockQty
        //Consider rename method name to GetStockOnHoldWithProductForCountOrderPrice
        public async Task<List<StockOnHoldWithProductForCountOrderPriceDto>> GetStockOnHoldWithProductForCharge(ISession session, List<ProductFromBasketCookieDto> basketProductsCookie)
        {
            var stockIds = basketProductsCookie.Select(s => s.StockId).ToList();

            var result = await _ctx.StockOnHolds.Where(s => stockIds.Contains(s.StockId) && s.SessionId == session.Id).Select(s => new StockOnHoldWithProductForCountOrderPriceDto()
            {
                StockId = s.StockId,
                //Consider removing ProductId from StockOnHold Entity
                ProductId = s.ProductId,
                //Consider changing Source of getting Price, because Price could be changed, and when someone already placed order should have same price when he placed it.
                Price = s.Stock.Product.Price,
                StockQty = s.StockQty
            }).ToListAsync();


            return result;                     
        }

    }
}