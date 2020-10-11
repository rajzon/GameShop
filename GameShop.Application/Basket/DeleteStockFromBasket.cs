using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GameShop.Application.Basket
{
    //TODO: Move out logic repsonsible for removing StockOnHold for that Session, think about reduce amount of parameters pass to method(for example use Dto)
    //Consider remove Session when Basket do not contain any Stock (I mean elements in Basket Cookie) instead setting string as Empty,
    //OR hold that Session for example 5 mins, and If any items is not added to That basket THEN Session is deleted[consider using Global Filter]
    public class DeleteStockFromBasket : IDeleteStockFromBasket
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStockFromBasket(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Do(ISession session, List<ProductFromBasketCookieDto> basketCookie, int stockIdToDelete)
        {
            var productToDelete = basketCookie.FirstOrDefault(b => b.StockId == stockIdToDelete);

            var stockOnHold = await _unitOfWork.StockOnHold.FindAsync(s => s.SessionId == session.Id && s.StockId == productToDelete.StockId);
            if (stockOnHold != null)
            {
                var stockToIncreaseQty = await _unitOfWork.Stock.GetAsync(stockOnHold.StockId);
                stockToIncreaseQty.Quantity += stockOnHold.StockQty;

                _unitOfWork.StockOnHold.Delete(stockOnHold);

                if (!await _unitOfWork.SaveAsync())
                {
                   return false;
                }
            }


            basketCookie.Remove(productToDelete);

            string basketJson;
            if (!basketCookie.Any())
            {
                basketJson = string.Empty;   
            } 
            else
            {
                basketJson = JsonConvert.SerializeObject(basketCookie);
            }

            session.SetString("Basket", basketJson);

            return true;
        }
    }
}