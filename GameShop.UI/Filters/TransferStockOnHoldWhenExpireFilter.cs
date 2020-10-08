using GameShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.UI.Filters
{
    public class TransferStockOnHoldWhenExpireFilter : IAsyncActionFilter
    {
        private readonly ITransferStockOnHoldWhenExpire _transferStockOnHoldWhenExpire;
        public TransferStockOnHoldWhenExpireFilter(ITransferStockOnHoldWhenExpire transferStockOnHoldWhenExpire)
        {

            _transferStockOnHoldWhenExpire = transferStockOnHoldWhenExpire;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!await _transferStockOnHoldWhenExpire.Do())
            {
                context.Result = new BadRequestObjectResult("Something went wrong during deleting Expired StockOnHold");
            }
            else
            {
                await next();
            }      
        }
    }

    public class TransferStockOnHoldWhenExpireAttribute : TypeFilterAttribute
    {
        public TransferStockOnHoldWhenExpireAttribute()
            :base(typeof(TransferStockOnHoldWhenExpireFilter))
        { 
        }
    }
}