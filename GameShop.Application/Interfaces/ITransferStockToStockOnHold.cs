using System.Threading.Tasks;
using GameShop.Domain.Enums;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace GameShop.Application.Interfaces
{
    public interface ITransferStockToStockOnHold
    {
         Task<StockOnHold> Do(TransferStockToStockOnHoldTypeEnum transferType ,ISession session, Stock stockToSubtract, int stockQty);
    }
}