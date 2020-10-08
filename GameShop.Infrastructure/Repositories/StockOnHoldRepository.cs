using System;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace GameShop.Infrastructure.Repositories
{
    public class StockOnHoldRepository : BaseRepository<StockOnHold>, IStockOnHoldRepository
    {
        public StockOnHoldRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
        }

    }
}