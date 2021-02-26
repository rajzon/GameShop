using System;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;
using System.Linq;
using GameShop.Domain.Enums;
using GameShop.Application.Helpers;
using Microsoft.Extensions.Options;

namespace GameShop.Application.Basket
{
    public class TransferStockToStockOnHold : ITransferStockToStockOnHold
    {
        private IUnitOfWork _unitOfWork;
        private readonly IOptions<BasketSettings> _basketOptions;

        public TransferStockToStockOnHold(IUnitOfWork unitOfWork, IOptions<BasketSettings> basketOptions)
        {
            _unitOfWork = unitOfWork;
            _basketOptions = basketOptions;
        } 
       
       //ToDO: Reduce amount of parameters, split logic repsonsible for increasing ExpireTime for already placed products in basket
       //ToDO: refactor , to many ifs
       //TODO: refactor , enums , for example TransferStockToStockOnHoldTypeEnum.One is not used
       //Consider not returning StockOnHold, because i do not want to do antything with that Stock, and also It can produce some issuse, 
       //because someone could add that Stock to Db that is already added in that method (line: 66)
       //Instead consider return bool,void(with Error/Status prop in class) or Dto but NO REAL STOCKONHOLD which is used by EF
        public async Task<StockOnHold> Do(TransferStockToStockOnHoldTypeEnum transferType ,ISession session, Stock stockToSubtract, int stockQty)
        {
            if (stockToSubtract == null)
            {
                return null;
            }

            if (stockToSubtract.Quantity < stockQty)
            {
                return null;
            }

            await ChangeExpireTimeForExistingStocksOnHold(transferType, session);

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
                    ExpireTime = DateTime.Now.AddMinutes(_basketOptions.Value.StockOnHoldExpireMinutes),
                    StockQty = stockQty,
                    //In The Future Remove That ProductId
                    ProductId = stockToSubtract.ProductId
                     
                };

                _unitOfWork.StockOnHold.Add(stockOnHoldToBeAdded);

            }
            stockToSubtract.Quantity -= stockQty;
            
            return stockOnHoldToBeAdded;

        }


        private async Task ChangeExpireTimeForExistingStocksOnHold(TransferStockToStockOnHoldTypeEnum transferType ,ISession session)
        {
            if (transferType == TransferStockToStockOnHoldTypeEnum.OneWithUpdatingExpireTimeForBasketProducts)
            {
                var stocksOnHoldToChangeExpireTime = await _unitOfWork.StockOnHold.FindAllAsync(s => s.SessionId == session.Id);

                if (stocksOnHoldToChangeExpireTime.Count() > 0)
                {
                    foreach (var stockOnHold in stocksOnHoldToChangeExpireTime)
                    {        
                        stockOnHold.ExpireTime = DateTime.Now.AddMinutes(_basketOptions.Value.StockOnHoldExpireMinutes);
                    }
                }
            }        
        }  
    }
}