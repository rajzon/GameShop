using GameShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.UI.Filters
{
    public class TransferStockOnHoldWhenExpireFilter : IAsyncActionFilter
    {
        private readonly ITransferStockOnHoldWhenExpire _transferStockOnHoldWhenExpire;
        private readonly ILogger _logger;

        public TransferStockOnHoldWhenExpireFilter(ITransferStockOnHoldWhenExpire transferStockOnHoldWhenExpire , ILogger<TransferStockOnHoldWhenExpireFilter> logger)
        {

            _transferStockOnHoldWhenExpire = transferStockOnHoldWhenExpire;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if(!await _transferStockOnHoldWhenExpire.Do())
            {
                // context.RouteData.Values.Add("IsTransferDataSucceed", false);
                // context.Result = new BadRequestObjectResult("Something went wrong during deleting Expired StockOnHold");
                _logger.LogWarning("Something went wrong during deleting Expired StockOnHold");
            }
            await next();
            
                 
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