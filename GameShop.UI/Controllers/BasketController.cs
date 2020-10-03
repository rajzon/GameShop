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
        private readonly IAddProductToBasket _addProductToBasket;
        private readonly ICountOrderPrice _countOrderPrice;

        public BasketController(IUnitOfWork unitOfWork, IAddProductToBasket addProductToBasket, ICountOrderPrice countOrderPrice)
        {
            _unitOfWork = unitOfWork;
            _addProductToBasket = addProductToBasket;
            _countOrderPrice = countOrderPrice;
        }
        [HttpPost("add-product/{id}")]
        public async Task<IActionResult> AddProductToBasket(int id, [FromQuery]int stockQty)
        {
            if (stockQty < 1)
            {
                return BadRequest("Quantity that was passed is less then 1");
            }

            var stockToAdd = await _unitOfWork.Stock.GetByProductId(id);
            if (stockToAdd == null)
            {
                return BadRequest("Stock for that Product don't exist");
            }

            _addProductToBasket.Do(HttpContext.Session, stockToAdd, stockQty);
                        
            return Ok($"Added product {id} to basket TEST");
        }



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

            var productsFromRepo = await _unitOfWork.Product.GetProductsForBasketAsync(basketCookie);

            decimal basketPrice = _countOrderPrice.Do(productsFromRepo);
            
            var basketToReturn = new BasketDto()
            {
                BasketPrice = basketPrice,
                BasketProducts = productsFromRepo
            };

            return Ok(basketToReturn);


        }
    }
}