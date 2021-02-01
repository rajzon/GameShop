using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
using GameShop.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameShop.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BasketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddStockToBasket _addStockToBasket;
        private readonly ICountOrderPrice _countOrderPrice;
        private readonly ITransferStockToStockOnHold _transferStockToStockOnHold;
        private readonly IDeleteStockFromBasket _deleteStockFromBasket;
        private readonly ISynchronizeBasket _synchronizeBasket;
        private readonly ITransferStockOnHoldWhenExpire _transferStockOnHoldWhenExpire;

        public BasketController(IUnitOfWork unitOfWork, IAddStockToBasket addProductToBasket, ICountOrderPrice countOrderPrice, 
                        ITransferStockToStockOnHold transferStockToStockOnHold, IDeleteStockFromBasket deleteStockFromBasket, ISynchronizeBasket synchronizeBasket,
                        ITransferStockOnHoldWhenExpire transferStockOnHoldWhenExpire)
        {
            _unitOfWork = unitOfWork;
            _addStockToBasket = addProductToBasket;
            _countOrderPrice = countOrderPrice;
            _transferStockToStockOnHold = transferStockToStockOnHold;
            _deleteStockFromBasket = deleteStockFromBasket;
            _synchronizeBasket = synchronizeBasket;
            _transferStockOnHoldWhenExpire = transferStockOnHoldWhenExpire;
        }

        // Delete ProductId from StockOnHold and from Everywher , the context is basket
        [HttpPost("add-stock/{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddStockToBasket(int id, [FromQuery]int stockQty)
        {
            if (stockQty < 1)
            {
                return BadRequest("Quantity that was passed is less then 1");
            }

            var stockToSubtract = await _unitOfWork.Stock.GetAsync(id);
            if (stockToSubtract == null)
            {
                return BadRequest("Stock for that Product don't exist");
            }

            if (stockToSubtract.Quantity < stockQty)
            {
                return BadRequest("Requested Stock Quantity is greater then Stock in Db");
            }
            var stockOnHoldToAdd =  await _transferStockToStockOnHold.Do(TransferStockToStockOnHoldTypeEnum.OneWithUpdatingExpireTimeForBasketProducts, HttpContext.Session, stockToSubtract, stockQty);

            //TODO: Implement some better validation
            if (stockOnHoldToAdd == null)
            {
                return BadRequest("TODO Implement some better validation");
            }
            

            _addStockToBasket.Do(HttpContext.Session, stockOnHoldToAdd);

            if(!await _unitOfWork.SaveAsync())
            {
                return BadRequest("Something went wrong during saving Stock To StockOnHold");
            }
                        
            return Ok($"Added product {id} to basket TEST");
        }


        //WARNING: Consider using _unitOfWork.Product.GetProductsForBasketAsync(basketCookie); Because using Stock to get Product info cause many Joins so it is not good in terms of Performance
        //!!OR Refactor _unitOfWork.Stock.GetStockWithProductForBasket() to not use that many Joins
        [HttpGet("get-basket")]
        [ProducesResponseType(typeof(BasketDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType( (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {  
            var basketJson = HttpContext.Session.GetString("Basket");
            if (string.IsNullOrEmpty(basketJson))
            {
                return NotFound();
            } 

            var basketCookie = JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);


            var stocksWithProductsFromRepo = await  _unitOfWork.Stock.GetStockWithProductForBasket(basketCookie);

            decimal basketPrice = _countOrderPrice.Do(stocksWithProductsFromRepo);
            
            var basketToReturn = new BasketDto()
            {
                BasketPrice = basketPrice,
                BasketProducts = stocksWithProductsFromRepo
            };

            return Ok(basketToReturn);


        }

        [HttpDelete("delete-stock/{stockId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteStockFromBasket(int stockId)
        {
            var basketJson = HttpContext.Session.GetString("Basket");

            var basketCookie = JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);

            if (!await _deleteStockFromBasket.Do(HttpContext.Session, basketCookie, stockId))
            {
                return BadRequest("Something went wrong during removing StockOnHold");
            }

            if ( string.IsNullOrEmpty(HttpContext.Session.GetString("Basket")) )
            {
                Response.Cookies.Delete("Basket");
            }

            return NoContent();
        }


        //TODO: Inside SynchronizeBasket class , create wrapper for result of method SynchronizeBasket.Do() which will contain Result (true- means that Db was updated but 
        //also can be that some Products in basket miss His StockOnHold , false- means Db  wasnt updated
        //because its not needed OR because StockInDb Was not enugh to assign to this product basket Stock.Quantity < productFromBaske.StockQty) and MissingStocks
        // If i Am gonna create that wrapper also i can implement Error property inside that wrapper, for errors like passed basketProducts list that is empty

        [HttpPost("synchronize-basket")]
        [ProducesResponseType(typeof(List<NotEnoughStockInfoDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType( (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EnsureThatBasketProductsAreInStockOnHold()
        {
            var basketJson = HttpContext.Session.GetString("Basket");
            if (string.IsNullOrEmpty(basketJson))
            {
                //Consider return text that contains more accurate info
                return NotFound();
            }

            var basketCookie = JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);

            if (await _synchronizeBasket.Do(HttpContext.Session, basketCookie))
            {
                if (!await _unitOfWork.SaveAsync())
                {
                    return BadRequest("Something went wrong during saving");
                }
            }

            if (_synchronizeBasket.MissingStocks.Any())
            {
                return BadRequest(_synchronizeBasket.MissingStocks);
            }

            return Ok();
    

        }

        [HttpDelete("clear")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string),(int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ClearBasket()
        {

            if (!await _unitOfWork.StockOnHold.SetExpiredForStocksOnHoldAsync(HttpContext.Session))
            {
                return NotFound();
            }

            if (!await _unitOfWork.SaveAsync())
            {
                return BadRequest("Something went wrong saving StocksOnHold expire time");
            }

            if (!await _transferStockOnHoldWhenExpire.Do())
            {
                return BadRequest("Something went wrong during transfering StockOnHold to Stock");
            }

            

            Response.Cookies.Delete("Basket");
            return NoContent();
        }
    }
}