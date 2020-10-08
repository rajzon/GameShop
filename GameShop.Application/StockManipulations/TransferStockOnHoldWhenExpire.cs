using System;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;

namespace GameShop.Application.StockManipulations
{
    public class TransferStockOnHoldWhenExpire : ITransferStockOnHoldWhenExpire
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferStockOnHoldWhenExpire(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<bool> Do()
        {
            var stockOnHoldsToTransfer = await _unitOfWork.StockOnHold.FindAllAsync(s => s.ExpireTime < DateTime.Now);

            bool result = true;

            if (stockOnHoldsToTransfer.Count() > 1)
            { 
                foreach (var stockOnHold in stockOnHoldsToTransfer)
                {
                    var stockToUpdate = await _unitOfWork.Stock.GetAsync(stockOnHold.StockId);
                    if (stockToUpdate == null)
                    {
                        throw new NullReferenceException("StockId from StockOnHold not exist in Stock Entity - that's unexpected"); 
                    }
                    stockToUpdate.Quantity += stockOnHold.StockQty;
                }

                _unitOfWork.StockOnHold.DeleteRange(stockOnHoldsToTransfer);

               result = await _unitOfWork.SaveAsync()? true : false;

               return result;
                
            }

            return result;
        }
    }
}