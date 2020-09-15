using System.Threading.Tasks;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        public Task<Stock> GetByProductId(int productId);
    }
}