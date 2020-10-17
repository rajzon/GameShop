using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.CustomerDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameShop.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PaymentController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountOrderPrice _countOrderPrice;
        private readonly ICreateCharge _createCharge;
        private readonly ISynchronizeBasket _synchronizeBasket;

        public PaymentController(IUnitOfWork unitOfWork, ICountOrderPrice countOrderPrice, ICreateCharge createCharge, ISynchronizeBasket synchronizeBasket)
        {
            _unitOfWork = unitOfWork;
            _countOrderPrice = countOrderPrice;
            _createCharge = createCharge;
            _synchronizeBasket = synchronizeBasket;
        }

        //TODO: Refactor it
        //TODO: Move maxLength Value Validation prop for CustomerInfoDto to JSON file config also move that value for OrderConfiguration prop
        //TODO: Change Return Type for SynchronizeBasket And for TransferStockTOStockOnHold
        //TODO: Check if StockOnHoldWithProd for calculate order price contains same StockId, and Same Qty(line 54)
        //TODO: Add filtering that selects StockOnHOld to create base on SessionId AND StockId from basket (line 72)
        [HttpPost("charge")]
        public async Task<IActionResult> Charge([FromHeader(Name="Stripe-Token")]string stripeToken, CustomerInfoDto customerInfo)
        {
            var basketJson = HttpContext.Session.GetString("Basket");

            if (string.IsNullOrEmpty(basketJson))
            {
                return BadRequest("Basket Cookie is empty");
            }

            var basketProductsCookie = JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);

            if(!await _synchronizeBasket.Do(HttpContext.Session, basketProductsCookie) && !_synchronizeBasket.MissingStocks.Any())
            {
                return BadRequest("Stocks in Basket are not able to be assign to that Session");
            }

            var productsFromRepo = await _unitOfWork.StockOnHold.GetStockOnHoldWithProductForCharge(HttpContext.Session, basketProductsCookie);

            if (productsFromRepo.Count < 1)
            {
                return BadRequest("Error occured during retrieving data products from Database");   
            }

            decimal basketPrice = _countOrderPrice.Do(productsFromRepo);

            var basketForPaymentDto = _createCharge.Do(stripeToken, basketPrice, basketProductsCookie);

            var order =  _unitOfWork.Order.ScaffoldOrderForCreation(customerInfo, basketForPaymentDto);

            // var stockIdWithQtyToRemove = productsFromRepo.ToDictionary(s => s.StockId, s => s.StockQty);
            
            // await _unitOfWork.Stock.RemoveStockQty(stockIdWithQtyToRemove);

            await _unitOfWork.StockOnHold.DeleteRange(s => s.SessionId == HttpContext.Session.Id);

           _unitOfWork.Order.Add(order);

           if (await _unitOfWork.SaveAsync())
           {           
               HttpContext.Session.Remove("Basket");
               //TODO Replace that
               return Ok(201);
           }

            
            return BadRequest("Something went wrong during saving Order");
        }
    }
}