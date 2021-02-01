using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.DeliveryOptDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DeliveryOptsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeliveryOptsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(DeliveryOptToReturnDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDeliveryOpts()
        {   
            var deliveryOpts = await _unitOfWork.DeliveryOpt.GetAllAsync();

            if (!deliveryOpts.Any())
            {
                return NotFound();
            }

            var deliveryOptsToReturn = _mapper.Map<List<DeliveryOptToReturnDto>>(deliveryOpts);

            return Ok(deliveryOptsToReturn);
        }  
    }
}