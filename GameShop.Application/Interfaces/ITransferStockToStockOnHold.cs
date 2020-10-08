using System.Threading.Tasks;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace GameShop.Application.Interfaces
{
    public interface ITransferStockToStockOnHold
    {
         Task<StockOnHold> Do(ISession session, Stock stockToSubtract, int stockQty);
    }
}