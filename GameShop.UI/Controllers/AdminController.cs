using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GameShop.Application.Helpers;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using GameShop.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GameShop.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGameShopRepository _repo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly IUnitOfWork _unitOfWork;

    public AdminController(ApplicationDbContext ctx, UserManager<User> userManager, IMapper mapper,
            IGameShopRepository repo, IOptions<CloudinarySettings> cloudinaryConfig, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryConfig = cloudinaryConfig;
        _repo = repo;
        _mapper = mapper;
        _userManager = userManager;
        _ctx = ctx;

            Account acc = new Account(_cloudinaryConfig.Value.CloudName,
            _cloudinaryConfig.Value.ApiKey,
            _cloudinaryConfig.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(acc);

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

        selectedRoles = selectedRoles ?? new string[] { };

        var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        if (!result.Succeeded)
        {
            return BadRequest("Fail during adding roles to specified user");
        }

        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
        if (!result.Succeeded)
        {
            return BadRequest("Fail during removing roles to specified user");
        }

        return Ok(await _userManager.GetRolesAsync(user));
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpGet("prodcuts-for-moderation")]
    public async Task<IActionResult> GetProductsForModeration([FromQuery]ProductParams productParams)
    {

        //var categoryId = (int)_ctx.Entry<Product>(await _ctx.Products.FirstOrDefaultAsync()).Property("CategoryId").CurrentValue;
        var products = await _repo.GetProductsForModerationAsync(productParams);

        var productsToReturn = _mapper.Map<IEnumerable<ProductForModerationDto>>(products);

        Response.AddPagination(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages);

        return Ok(productsToReturn);
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpGet("product-for-edit/{id}")]
    public async Task<IActionResult> GetProductForEdit(int id)
    {
        var requirements = await _ctx.Requirements.GetRequirementsForProduct(id).FirstOrDefaultAsync();

        if (requirements == null)
        {
            return BadRequest("Error occured during retrieving requirements for selected product");
        }

        var photosFromRepo = await _unitOfWork.Photo.GetPhotosForProduct(id);

        if (photosFromRepo == null)
        {
            return BadRequest("Error occured during retrieving photos for selected product");
        }

        var product = await _ctx.Products.GetProductForEdit(_ctx, requirements, photosFromRepo, id).FirstOrDefaultAsync();

        if (product == null)
        {
            return BadRequest("Error occured during retrieving product");
        }


        return Ok(product);
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpPost("create-product")]
    public async Task<IActionResult> CreateProduct(ProductForCreationDto productForCreationDto)
    {

        if (productForCreationDto == null)
        {
            return BadRequest("Sended blank product description");
        }

        var selectedCategory = await _unitOfWork.Category.GetAsync(productForCreationDto.CategoryId);

        var requirementsToUpdate = _mapper.Map<Requirements>(productForCreationDto.Requirements);

        var prodcuts = await _repo.CreateProduct(productForCreationDto, requirementsToUpdate, selectedCategory);

        return Ok(await _ctx.Products.OrderByDescending(p => p.Id).FirstOrDefaultAsync());
    }


    [Authorize(Policy = "ModerateProductRole")]
    [HttpPost("edit-product/{id}")]
    public async Task<IActionResult> EditProduct(int id, ProductToEditDto productToEditDto)
    {

        var productFromDb = await _repo.GetProduct(id);

        var selectedCategory = await _ctx.Categories.FirstOrDefaultAsync(c => c.Id == productToEditDto.CategoryId);


        var requirementsToUpdate = _mapper.Map<Requirements>(productToEditDto.Requirements);

        var updatedProduct = await _repo.EditProduct(id, productToEditDto, requirementsToUpdate, selectedCategory, productFromDb);

        productFromDb.Id = updatedProduct.Id;
        productFromDb.Name = updatedProduct.Name;
        productFromDb.Description = updatedProduct.Description;
        productFromDb.Pegi = updatedProduct.Pegi;
        productFromDb.Price = updatedProduct.Price;
        productFromDb.IsDigitalMedia = updatedProduct.IsDigitalMedia;
        productFromDb.ReleaseDate = updatedProduct.ReleaseDate;
        productFromDb.Category = selectedCategory;
        productFromDb.Languages = updatedProduct.Languages;
        productFromDb.Requirements = requirementsToUpdate;
        productFromDb.SubCategories = updatedProduct.SubCategories;

        //var productToUpdate = _mapper.Map<Product>(updatedProduct);          


        // productFromDb.Languages = updatedProduct.Languages;

        // productFromDb.Requirements = requirementsToUpdate;
        // //_ctx.Entry(productFromDb.Requirements).State = EntityState.Modified;
        // productFromDb.SubCategories = updatedProduct.SubCategories;

        //  productFromDb.Photos = updatedProduct.Photos;




        //_ctx.Entry(productFromDb).State = EntityState.Modified;
        // _ctx.Update(productFromDb);
        //  _ctx.Entry(productFromDb).CurrentValues.SetValues(productToUpdate);            
        if (await _repo.SaveAll())
        {
            return Ok(await _ctx.Products.FindAsync(id));
        }

        return BadRequest("Fail occured during editing Product");
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpDelete("delete-product/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {

        var selectedProduct = await _repo.GetProduct(id);

        if (selectedProduct == null)
        {
            return BadRequest("Product for that Id doesn't exist");
        }

        var photos = selectedProduct.Photos.ToList();

        if (photos.Select(p => p.PublicId).Any())
        {

            foreach (var photo in photos)
            {
                if (photo.PublicId != null)
                {
                    var deleteParams = new DeletionParams(photo.PublicId);

                    var result = _cloudinary.Destroy(deleteParams);

                    if (result.Result == "ok")
                    {
                        _repo.Delete(photo);
                    }
                }
                else
                {
                    _repo.Delete(photo);
                }
            }

        }
        _repo.Delete(selectedProduct);


        if (!await _repo.SaveAll())
        {
            return BadRequest("Something went wrong during deleting product");
        }
        //"Product deleted successfully" problem with parsing , ui try parse this to JSON instead of keep this as text
        return Ok();
    }


    [Authorize(Policy = "ModerateProductRole")]
    [HttpGet("available-categories")]
    public async Task<IActionResult> GetCategories()
    {

        var categoriesList = await _unitOfWork.Category.GetAllOrderedByAsync(x => x.Id);

        if (categoriesList == null || !categoriesList.Any())
        {
            return BadRequest("There is no categories in Database");
        }

        var categoryToRetrun = _mapper.Map<IEnumerable<CategoryToReturnDto>>(categoriesList);

        return Ok(categoryToRetrun);
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpGet("available-subCategories")]
    public async Task<IActionResult> GetSubCategories()
    {

        var subCategoriesList = await _unitOfWork.SubCategory.GetAllAsync();

        if (subCategoriesList == null || !subCategoriesList.Any())
        {
            return BadRequest("There is no subCategories in Database");
        }

        var subCategoryToRetrun = _mapper.Map<IEnumerable<SubCategoryToReturnDto>>(subCategoriesList);

        return Ok(subCategoryToRetrun);
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpGet("available-languages")]
    public async Task<IActionResult> GetLanguages()
    {

        var languagesList = await _unitOfWork.Language.GetAllOrderedByAsync(x => x.Id);

        if (languagesList == null || !languagesList.Any())
        {
            return BadRequest("There is no languages in Database");
        }

        var languagesListToReturn = _mapper.Map<IEnumerable<LanguagesToReturnDto>>(languagesList);

        return Ok(languagesListToReturn);
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpGet("category-test/{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _unitOfWork.Category.GetAsync(id);
        if (category == null)
        {
            return BadRequest("That Category doesn't exist");
        }

        return Ok(category);

    }



}
}