using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.StockDto;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        Task<Stock> GetByProductId(int productId);
    }
}