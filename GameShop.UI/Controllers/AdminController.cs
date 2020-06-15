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
        public AdminController(ApplicationDbContext ctx, UserManager<User> userManager, IMapper mapper, 
                IGameShopRepository repo, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _ctx = ctx;

             Account acc = new Account (_cloudinaryConfig.Value.CloudName,
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
        public async Task<IActionResult> GetProductsForModeration()
        {

            //var categoryId = (int)_ctx.Entry<Product>(await _ctx.Products.FirstOrDefaultAsync()).Property("CategoryId").CurrentValue;
            var productList = await _ctx.Products.OrderBy(x => x.Id)
                .Select(product => new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ReleaseDate = product.ReleaseDate,
                    SubCategories = (from productSubCategory in product.SubCategories
                                     join subCategory in _ctx.SubCategories
                                     on productSubCategory.SubCategoryId
                                     equals subCategory.Id
                                     select subCategory.Name).ToList(),
                    CategoryName = _ctx.Categories.FirstOrDefault(c => c.Id == EF.Property<int>(product, "CategoryId")).Name,
                }).ToListAsync();


            return Ok(productList);
        }

        [Authorize(Policy = "ModerateProductRole")]
        [HttpGet("product-for-edit/{id}")]
        public async Task<IActionResult> GetProductForEdit(int id)
        {
            var requirements = await _ctx.Requirements.Where(r => r.ProductId == id)
                        .Select(requriements => new RequirementsForEditDto
                        {
                            OS = requriements.OS,
                            Processor = requriements.Processor,
                            RAM = requriements.RAM,
                            GraphicsCard = requriements.GraphicsCard,
                            HDD = requriements.HDD,
                            IsNetworkConnectionRequire = requriements.IsNetworkConnectionRequire
                        }).FirstOrDefaultAsync();

            var photosFromRepo = await _repo.GetPhotosForProduct(id);

            var productList = await _ctx.Products.Where(x => x.Id == id)
                .Select(product => new ProductToEditDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    Pegi = product.Pegi,
                    Price = product.Price,
                    ReleaseDate = product.ReleaseDate,
                    IsDigitalMedia = product.IsDigitalMedia,
                    SubCategoriesId = (from productSubCategory in product.SubCategories
                                       join subCategory in _ctx.SubCategories
                                       on productSubCategory.SubCategoryId
                                       equals subCategory.Id
                                       select subCategory.Id).ToList(),
                    LanguagesId = (from productLangauge in product.Languages
                                   join language in _ctx.Languages
                                   on productLangauge.LanguageId
                                   equals language.Id
                                   select language.Id).ToList(),
                    Requirements = requirements,
                    CategoryId = _ctx.Categories.FirstOrDefault(c => c.Id == EF.Property<int>(product, "CategoryId")).Id,
                    Photos = photosFromRepo
                }).FirstOrDefaultAsync();


            return Ok(productList);
        }

        [Authorize(Policy = "ModerateProductRole")]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(ProductForCreationDto productForCreationDto)
        {

            if (productForCreationDto == null)
            {
                return BadRequest("Sended blank product description");
            }

            var selectedCategory = await _repo.GetCategory(productForCreationDto.CategoryId);

            var requirementsToUpdate = _mapper.Map<Requirements>(productForCreationDto.Requirements);

            var prodcuts = await _repo.CreateProduct(productForCreationDto, requirementsToUpdate, selectedCategory);

            return Ok(await _ctx.Products.OrderByDescending(p => p.Id).FirstOrDefaultAsync());
        }


        [Authorize(Policy = "ModerateProductRole")]
        [HttpPost("edit-product/{id}")]
        public async Task<IActionResult> EditProduct(int id, ProductToEditDto productToEditDto)
        {

            var productFromDb = await _repo.GetProductForEdit(id);

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

            var selectedProduct = await _repo.GetProductForDelete(id);

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

            var categoriesList = await _repo.GetCategories();

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

            var subCategoriesList = await _repo.GetSubCategories();

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

            var languagesList = await _repo.GetLanguages();

            if (languagesList == null || !languagesList.Any())
            {
                return BadRequest("There is no languages in Database");
            }

            var languagesListToReturn = _mapper.Map<IEnumerable<LanguagesToReturnDto>>(languagesList);

            return Ok(languagesListToReturn);
        }



    }
}