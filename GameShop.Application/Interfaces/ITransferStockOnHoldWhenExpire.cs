using System.Threading.Tasks;

namespace GameShop.Application.Interfaces
{
    public interface ITransferStockOnHoldWhenExpire
    {
         Task<bool> Do();
    }
}