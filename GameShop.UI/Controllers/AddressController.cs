using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.AddressDtos;
using GameShop.UI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}/user/{userId}", Name = "GetAddress")]
        [AdminOrUserWithSameUserIdFilter("userId")]
        public async Task<IActionResult> GetAddress(int id, int userId)
        {
        
            var address = await _unitOfWork.Address.FindAsync(a => a.Id == id && a.UserId == userId);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpGet("user/{userId}")]
        [AdminOrUserWithSameUserIdFilter("userId")]
        public async Task<IActionResult> GetAddressesForUser(int userId)
        {
            var addresses = await _unitOfWork.Address.FindAllAsync(x => x.UserId == userId);

            var addressesToReturn = _mapper.Map<IEnumerable<UserAddressesForListDto>>(addresses);

            if (!addresses.Any())
            {
                return NotFound();
            }

            return Ok(addressesToReturn);
        }

        [HttpDelete("delete/{id}/user/{userId}")]
        [AdminOrUserWithSameUserIdFilter("userId")]
        public async Task<IActionResult> DeleteAddress(int id, int userId)
        {
            var address = await _unitOfWork.Address.FindAsync(a => a.Id == id && a.UserId == userId);

            if (address == null)
            {
                return NotFound();
            }

            _unitOfWork.Address.Delete(address);

            if (!await _unitOfWork.SaveAsync())
            {
                return BadRequest("Something went wrong during deleting address");
            }

            return NoContent();
        }
    }
}