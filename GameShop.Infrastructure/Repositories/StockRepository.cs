using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.StockDto;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Repositories
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(ApplicationDbContext ctx)
            :base(ctx)
        {

        }

        public async Task<Stock> GetByProductId(int productId)
        {
            return await _ctx.Stocks.Where(s => s.ProductId == productId)
                                .FirstOrDefaultAsync();
        }



        public async Task<List<StockWithProductForBasketDto>> GetStockWithProductForBasket(List<ProductFromBasketCookieDto> basketCookie)
        {
            var stockIds = basketCookie.Select(bc => bc.StockId).ToList();
            var result = await _ctx.Stocks.Where(s => stockIds.Contains(s.Id)).Include(s => s.Product).Select(s => new StockWithProductForBasketDto()
            {
                //ToDO: Remove ProductID from here
                ProductId = s.ProductId,
                StockId = s.Id,
                Name = s.Product.Name,
                CategoryName = _ctx.Categories.FirstOrDefault(c => c.Id == EF.Property<int>(s.Product, "CategoryId")).Name,
                Price = s.Product.Price
            }).ToListAsync();
            foreach (var basketProduct in basketCookie)
            {
                result.FirstOrDefault(s => s.StockId == basketProduct.StockId).StockQty = basketProduct.StockQty;
            }

            return result;
        }

        public async Task<List<StockWithProductForCountOrderPrice>> GetStockWithProductForCharge(List<ProductFromBasketCookieDto> basketProductsCookie)
        {
            var stockIds = basketProductsCookie.Select(s => s.StockId).ToList();

            var result = await _ctx.Stocks.Where(s => stockIds.Contains(s.Id)).Select(s => new StockWithProductForCountOrderPrice()
            {
                StockId = s.Id,
                ProductId = s.ProductId,
                Price = s.Product.Price,
                StockQty = s.Quantity
            }).ToListAsync();
            
            foreach (var item in basketProductsCookie)
            {
               int stockToUpdate = result.FirstOrDefault(p => p.StockId == item.StockId).StockQty;
               if (stockToUpdate < item.StockQty)
               {
                   throw new ArgumentException("Requested Stock Quantity is greater then Stock in Db");
               }      
            };
            return result;
        }

        public async Task RemoveStockQty(Dictionary<int,int> stockIdWithQtyToRemove)
        {
            var dictionaryKey = stockIdWithQtyToRemove.Keys.ToList();
            var stocksToModerate = await _ctx.Stocks.Where(s => dictionaryKey.Contains(s.Id)).ToListAsync();
            foreach (var item in stockIdWithQtyToRemove)
            {
                stocksToModerate.FirstOrDefault(s => s.Id == item.Key).Quantity -= item.Value;
            }
        
        }
        
        public async Task<Stock> FindWithProductAsync(Expression<Func<Stock, bool>> expression)
        {
            return await _ctx.Stocks.Include(s => s.Product).Where(expression).FirstOrDefaultAsync();
        }

    }
}