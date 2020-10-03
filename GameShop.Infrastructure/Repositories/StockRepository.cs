using System.Collections.Generic;
using System.Linq;
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

    }
}