using System.Linq;
using System.Threading.Tasks;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<User> _userManager;
        public AdminController(ApplicationDbContext ctx, UserManager<User> userManager)
        {
            _userManager = userManager;
            _ctx = ctx;

        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await _ctx.Users.OrderBy(x => x.UserName)
                .Select(user => new
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = (from userRole in user.UserRoles
                             join role in _ctx.Roles
                             on userRole.RoleId
                             equals role.Id
                             select role.Name).ToList()
                }).ToListAsync();

            return Ok(userList);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] {};

            var result = await _userManager.AddToRolesAsync(user,selectedRoles.Except(userRoles));
            if(!result.Succeeded)
            {
                return BadRequest("Fail during adding roles to specified user");
            }

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if(!result.Succeeded)
            {
                return BadRequest("Fail during removing roles to specified user");
            }

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "ModerateProductRole")]
        [HttpGet("prodcuts-for-moderation")]
        public async Task<IActionResult> GetProductsForModeration()
        {
            var productList = await _ctx.Products.OrderBy(x => x.Id)
                .Select(product => new
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Pegi = product.Pegi,
                        Price = product.Price,
                        IsDigitalMedia = product.IsDigitalMedia,
                        ReleaseDate = product.ReleaseDate,
                        SubCategories = (from productSubCategory in product.SubCategories
                                        join subCategory in _ctx.SubCategories
                                        on productSubCategory.SubCategoryId
                                        equals subCategory.Id
                                        select subCategory.Name).ToList()
                    }).ToListAsync(); 

            return Ok(productList);
        }

    }
}