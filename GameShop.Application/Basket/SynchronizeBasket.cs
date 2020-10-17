using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Helpers;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Enums;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GameShop.Application.Basket
{
    public class SynchronizeBasket : ISynchronizeBasket
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransferStockToStockOnHold _transferStockToStockOnHold;
        private readonly IOptions<BasketSettings> _basketSettings;

        public List<NotEnoughStockInfoDto> MissingStocks { get; private set; }

        public SynchronizeBasket(IUnitOfWork unitOfWork, ITransferStockToStockOnHold transferStockToStockOnHold, IOptions<BasketSettings> basketSettings)
        {
            _unitOfWork = unitOfWork;
            _transferStockToStockOnHold = transferStockToStockOnHold;
            _basketSettings = basketSettings;

            MissingStocks = new List<NotEnoughStockInfoDto>();
        }

        //Consider: Create wrapper for response which will contain Errors, Resutlt[true,false] and MissingStocks
        //Consider put Wrapper class as internal class or nested class of that class
        //Consider not allowing Saving Db when Any missing stock occured
        //I Should consider if i need logic responsible for increase ExpireTime for existing StockOnHold(from line 44)  when I use that method for synchronizing Basket during Charge
        public async Task<bool> Do(ISession session, List<ProductFromBasketCookieDto> basketCookie)
        {
            if (basketCookie == null || !basketCookie.Any())
            {
                throw new ArgumentException("passed product list from basket that is null or empty");
            }

            var stocksOnHoldForThatBasket = await _unitOfWork.StockOnHold.FindAllAsync(s => s.SessionId == session.Id);
            var stockIdsFromStockOnHold = stocksOnHoldForThatBasket.Select(s => s.StockId).ToList();
            foreach (var product in basketCookie)
            {
                if (stockIdsFromStockOnHold.Contains(product.StockId))
                {                  
                    var stockOnHoldThatIsAssignedToThatBasekt = stocksOnHoldForThatBasket.FirstOrDefault(s => s.StockId == product.StockId);
                    
                    stockOnHoldThatIsAssignedToThatBasekt.ExpireTime = DateTime.Now.AddMinutes(_basketSettings.Value.StockOnHoldExpireMinutes);
                }
                else
                {
                    var stockToAttemptToGetForBasket = await _unitOfWork.Stock.FindWithProductAsync(s => s.Id == product.StockId);

                    if (stockToAttemptToGetForBasket == null)
                    {
                        throw new ArgumentException("StockId from Basket not exist in Stock Entity");
                    }

                    if (stockToAttemptToGetForBasket.Quantity < product.StockQty)
                    {
                        AddMissingStockToList(stockToAttemptToGetForBasket);
                    } 
                    else
                    {
                        await _transferStockToStockOnHold.Do(TransferStockToStockOnHoldTypeEnum.One, session, stockToAttemptToGetForBasket, product.StockQty);
                    }
                    
                }
            }

            if (_unitOfWork.IsAnyEntityAdded() || _unitOfWork.IsAnyEntityModified())
            {
                return true;
            }

            return false;
            
        }

        private void AddMissingStockToList(Stock stockThatHasNotEnoughQty)
        {
            var missingStock = new NotEnoughStockInfoDto()
            {
                StockId = stockThatHasNotEnoughQty.Id,
                ProductName = stockThatHasNotEnoughQty.Product.Name,
                AvailableStockQty = stockThatHasNotEnoughQty.Quantity                    
            };

            MissingStocks.Add(missingStock);
        }
    }
}