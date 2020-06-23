using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameShop.Application.Helpers;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.UI.Controllers
{

    [ApiController]
    [AllowAnonymous]  
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGameShopRepository _repo;
        private readonly IMapper _mapper;

        public ProductsController(IGameShopRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }


        [HttpGet]
        public async Task<IActionResult> GetProductsForSearching([FromQuery]ProductParams productParams) 
        {
            var products = await _repo.GetProductsForSearchingAsync(productParams);

            var productToReturn = _mapper.Map<IEnumerable<ProductForSearchingDto>>(products);

            Response.AddPagination(products.CurrentPage, products.PageSize, 
                            products.TotalCount, products.TotalPages); 


            return Ok(productToReturn);
        }

    }
}