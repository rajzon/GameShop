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
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly IUnitOfWork _unitOfWork;

    public AdminController(UserManager<User> userManager, IMapper mapper,
             IOptions<CloudinarySettings> cloudinaryConfig, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryConfig = cloudinaryConfig;
        _mapper = mapper;
        _userManager = userManager;
        

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
        var userList = await _unitOfWork.User.GetAllWithRolesAsync();

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
        var products = await _unitOfWork.Product.GetProductsForModerationAsync(productParams);

        var productsToReturn = _mapper.Map<IEnumerable<ProductForModerationDto>>(products);

        Response.AddPagination(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages);

        return Ok(productsToReturn);
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpGet("product-for-edit/{id}")]
    public async Task<IActionResult> GetProductForEdit(int id)
    {
        var requirements = await _unitOfWork.Requirements.GetRequirementsForProductAsync(id);

        if (requirements == null)
        {
            return BadRequest("Error occured during retrieving requirements for selected product");
        }

        var photosFromRepo = await _unitOfWork.Photo.GetPhotosForProduct(id);

        if (photosFromRepo == null)
        {
            return BadRequest("Error occured during retrieving photos for selected product");
        }

        var productToEdit = await _unitOfWork.Product.GetProductToEditAsync(requirements, photosFromRepo, id);

        if (productToEdit == null)
        {
            return BadRequest("Error occured during retrieving product");
        }


        return Ok(productToEdit);
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

        var requirementsForProduct = _mapper.Map<Requirements>(productForCreationDto.Requirements);

        var productToCreate = await _unitOfWork.Product.CreateAsync(productForCreationDto, requirementsForProduct, selectedCategory);

        _unitOfWork.Product.Add(productToCreate);
        await _unitOfWork.SaveAsync();

        return CreatedAtRoute("GetProduct", new {id = productToCreate.Id}, productToCreate);
    }


    [Authorize(Policy = "ModerateProductRole")]
    [HttpPost("edit-product/{id}")]
    public async Task<IActionResult> EditProduct(int id, ProductToEditDto productToEditDto)
    {
        if (productToEditDto == null)
        {
            return BadRequest("Sended blank product descirption");
        }

        var productFromDb = await _unitOfWork.Product.GetAsync(id);

        var selectedCategory = await _unitOfWork.Category.FindAsync(c => c.Id == productToEditDto.CategoryId);


        var requirementsToUpdate = _mapper.Map<Requirements>(productToEditDto.Requirements);

        var updatedProduct = await _unitOfWork.Product.EditAsync(id, productToEditDto, requirementsToUpdate, selectedCategory, productFromDb);

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
        if (await _unitOfWork.SaveAsync())
        {
            var editedProduct =  await _unitOfWork.Product.GetAsync(id);
            return CreatedAtRoute("GetProduct", new {id = id}, editedProduct);
        }

        return BadRequest("Fail occured during editing Product");
    }

    [Authorize(Policy = "ModerateProductRole")]
    [HttpDelete("delete-product/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {

        var selectedProduct = await _unitOfWork.Product.GetAsync(id);

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
                        _unitOfWork.Photo.Delete(photo);
                    }
                }
                else
                {
                    _unitOfWork.Photo.Delete(photo);
                }
            }

        }
        _unitOfWork.Product.Delete(selectedProduct);


        if (!await _unitOfWork.SaveAsync())
        {
            return BadRequest("Something went wrong during deleting product");
        }
        //"Product deleted successfully" problem with parsing , ui try parse this to JSON instead of keep this as text
        return NoContent();
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