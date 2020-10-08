using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Dtos.CustomerDto;
using GameShop.Domain.Dtos.PaymentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;

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

        public PaymentController(IUnitOfWork unitOfWork, ICountOrderPrice countOrderPrice, ICreateCharge createCharge)
        {
            _unitOfWork = unitOfWork;
            _countOrderPrice = countOrderPrice;
            _createCharge = createCharge;
        }

        //TODO: Refactor it
        [HttpPost("charge")]
        public async Task<IActionResult> Charge([FromHeader(Name="Stripe-Token")]string stripeToken, CustomerInfoDto customerInfo)
        {
            var basketJson = HttpContext.Session.GetString("Basket");

            if (string.IsNullOrEmpty(basketJson))
            {
                return BadRequest("Basket Cookie is empty");
            }

            var basketProductsCookie = JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);

            var productsFromRepo = await _unitOfWork.Stock.GetStockWithProductForCharge(basketProductsCookie);

            if (productsFromRepo.Count < 1)
            {
                return BadRequest("Error occured during retrieving data products from Database");   
            }

            decimal basketPrice = _countOrderPrice.Do(productsFromRepo);

            var basketForPaymentDto = _createCharge.Do(stripeToken, basketPrice, basketProductsCookie);

            var order =  _unitOfWork.Order.ScaffoldOrderForCreation(customerInfo, basketForPaymentDto);

            var stockIdWithQtyToRemove = productsFromRepo.ToDictionary(s => s.StockId, s => s.StockQty);
            
            await _unitOfWork.Stock.RemoveStockQty(stockIdWithQtyToRemove);

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