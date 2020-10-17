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

     

        
        public async Task<Stock> FindWithProductAsync(Expression<Func<Stock, bool>> expression)
        {
            return await _ctx.Stocks.Include(s => s.Product).Where(expression).FirstOrDefaultAsync();
        }

    }
}