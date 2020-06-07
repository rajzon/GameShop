using System.Threading.Tasks;
using AutoMapper;
using GameShop.Application.Interfaces;
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
        public async Task<IActionResult> GetProductsForSearching() 
        {
            var productsToReturn = await _repo.GetProductsForSearchingAsync(); 


            return Ok(productsToReturn);
        }

    }
}