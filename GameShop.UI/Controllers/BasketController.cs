using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.BasketDtos;
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

        public BasketController(IUnitOfWork unitOfWork, IAddStockToBasket addProductToBasket, ICountOrderPrice countOrderPrice, 
                        ITransferStockToStockOnHold transferStockToStockOnHold, IDeleteStockFromBasket deleteStockFromBasket)
        {
            _unitOfWork = unitOfWork;
            _addStockToBasket = addProductToBasket;
            _countOrderPrice = countOrderPrice;
            _transferStockToStockOnHold = transferStockToStockOnHold;
            _deleteStockFromBasket = deleteStockFromBasket;
        }

        // Delete ProductId from StockOnHold and from Everywher , the context is basket
        [HttpPost("add-stock/{id}")]
        [ProducesResponseType(typeof(string),400)]
        [ProducesResponseType(typeof(string),200)]
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
            var stockOnHoldToAdd =  await _transferStockToStockOnHold.Do(HttpContext.Session, stockToSubtract, stockQty);

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
        [ProducesResponseType(typeof(BasketDto),200)]
        [ProducesResponseType(404)]
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
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteStockFromBasket(int stockId)
        {
            var basketJson = HttpContext.Session.GetString("Basket");

            var basketCookie = JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);

            if (!await _deleteStockFromBasket.Do(HttpContext.Session, basketCookie, stockId))
            {
                return BadRequest("Something went wrong during removing StockOnHold");
            }

            return NoContent();
        }

        [HttpPost("synchronize-basket")]
        public async Task<IActionResult> EnsureThatBasketProductsAreInStockOnHold()
        {
            var basketJson = HttpContext.Session.GetString("Basket");

            var basketCookie = JsonConvert.DeserializeObject<List<ProductFromBasketCookieDto>>(basketJson);


            //TODO: MOVE THIS OUT

            //TODO: Make Dto for storing products That are not able to be added to Basket(because Stock Quantity for that product is = 0)
            var missingProductsListToReturn = new List<string>();

            var stockIdsFromBasket = basketCookie.Select(b => b.StockId);
            var stockOnHoldForThatBasket = await _unitOfWork.StockOnHold.FindAllAsync(s => s.SessionId == HttpContext.Session.Id);
            var stockIdsFromStockOnHold = stockOnHoldForThatBasket.Select(s => s.StockId);
            foreach (var product in basketCookie)
            {
                if (!stockIdsFromStockOnHold.Contains(product.StockId))
                {
                    //IT Wont Work because i need to Include missing Entity(Product)
                    var stockWithProductForErrorList = await _unitOfWork.Stock.GetAsync(product.StockId);
                    missingProductsListToReturn.Add(stockWithProductForErrorList.Product.Id+": " + stockWithProductForErrorList.Product.Name);

                    //TODO: Add Funcionality which will be reponsible for attempting to get StockOnHold From Stock (Stock -> StockOnHold -> apply to basket Cookie)
                }
            }

            return Ok();
            ////

        }
    }
}