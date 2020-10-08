using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace GameShop.Application.Interfaces
{
    public interface IAddStockToBasket
    {
        void Do(ISession session, StockOnHold stockToUpdate);
    }
}