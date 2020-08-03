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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        [HttpGet]
        public async Task<IActionResult> GetProductsForSearching([FromQuery]ProductParams productParams) 
        {
            var products = await _unitOfWork.Product.GetProductsForSearchingAsync(productParams);

            var productToReturn = _mapper.Map<IEnumerable<ProductForSearchingDto>>(products);

            Response.AddPagination(products.CurrentPage, products.PageSize, 
                            products.TotalCount, products.TotalPages); 


            return Ok(productToReturn);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(int id) 
        {
            var product = await _unitOfWork.Product.GetAsync(id);


            return Ok(product);
        }

    }
}