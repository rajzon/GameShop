using System;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace GameShop.Application.Basket
{
    public class TransferStockToStockOnHold : ITransferStockToStockOnHold
    {
        private IUnitOfWork _unitOfWork;

        public TransferStockToStockOnHold(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        public async Task<StockOnHold> Do(ISession session, Stock stockToSubtract, int stockQty)
        {
            if (stockToSubtract == null)
            {
                return null;
            }

            if (stockToSubtract.Quantity < stockQty)
            {
                return null;
            }

            var stocksOnHoldToChangeExpireTime = await _unitOfWork.StockOnHold.FindAllAsync(s => s.SessionId == session.Id);

            if (stocksOnHoldToChangeExpireTime.Count() > 1)
            {
                foreach (var stockOnHold in stocksOnHoldToChangeExpireTime)
                {        
                    stockOnHold.ExpireTime = DateTime.Now.AddMinutes(30);
                }
            }  
            var stockOnHoldToBeAdded = await _unitOfWork.StockOnHold.FindAsync(s => s.SessionId == session.Id && s.StockId == stockToSubtract.Id);

            if (stockOnHoldToBeAdded != null)
            {                
                stockOnHoldToBeAdded.StockQty += stockQty;            
            }
            else
            {
                stockOnHoldToBeAdded = new StockOnHold()
                {
                    SessionId = session.Id,
                    StockId = stockToSubtract.Id,
                    ExpireTime = DateTime.Now.AddMinutes(30),
                    StockQty = stockQty,
                    //In The Future Remove That ProductId
                    ProductId = stockToSubtract.ProductId
                     
                };

                _unitOfWork.StockOnHold.Add(stockOnHoldToBeAdded);

            }
            stockToSubtract.Quantity -= stockQty;
            

            return stockOnHoldToBeAdded;

        }
    }
}