using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace GameShop.Application.Interfaces
{
    public interface IAddProductToBasket
    {
         void Do(ISession session, Stock stockToAdd, int stockQty);
    }
}