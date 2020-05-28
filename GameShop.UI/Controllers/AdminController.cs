using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public AdminController(ApplicationDbContext ctx, UserManager<User> userManager, IMapper mapper)
        {
            _mapper = mapper;
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

            var categoryId = (int)_ctx.Entry<Product>(await _ctx.Products.FirstOrDefaultAsync()).Property("CategoryId").CurrentValue;
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
                                     select subCategory.Name).ToList(),
                    Languages = (from productLanguage in product.Languages
                                 join language in _ctx.Languages
                                 on productLanguage.LanguageId
                                 equals language.Id
                                 select language.Name).ToList(),
                    CategoryName = _ctx.Categories.FirstOrDefault(c => c.Id == EF.Property<int>(product, "CategoryId")).Name,
                    PhotosUrl = _ctx.Photos.Where(p => p.ProductId == product.Id).ToList()

                }).ToListAsync();


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

            //var populateProdductLanguage = _ctx.ProductsLanaguages.add

            var selectedCategory = await _ctx.Categories.FirstOrDefaultAsync(c => c.Id == productForCreationDto.CategoryId);

            var requirementsForProduct = new Requirements
            {
                OS = productForCreationDto.Requirements.OS,
                HDD = productForCreationDto.Requirements.HDD,
                Processor = productForCreationDto.Requirements.Processor,
                GraphicsCard = productForCreationDto.Requirements.GraphicsCard,
                IsNetworkConnectionRequire = productForCreationDto.Requirements.IsNetworkConnectionRequire,
                RAM = productForCreationDto.Requirements.RAM,
            };

            var product = new Product
            {
                Name = productForCreationDto.Name,
                Description = productForCreationDto.Description,
                Pegi = productForCreationDto.Pegi,
                Price = productForCreationDto.Price,
                IsDigitalMedia = productForCreationDto.IsDigitalMedia,
                ReleaseDate = productForCreationDto.ReleaseDate,
                Category = selectedCategory,
                Requirements = requirementsForProduct,
                Languages = new List<ProductLanguage>(),
                Photos = new List<Photo>(),
                SubCategories = new List<ProductSubCategory>()
            };

            foreach (var languageId in productForCreationDto.LanguagesId)
            {

                var selectedLanguages = await _ctx.Languages.FirstOrDefaultAsync(l => l.Id == languageId);
                var pl = new ProductLanguage { Product = product, Language = selectedLanguages };
                product.Languages.Add(pl);

            }
            foreach (var photo in productForCreationDto.Photos)
            {
                var photoToCreate = new Photo { Url = photo };
                product.Photos.Add(photoToCreate);
            }

            foreach (var subCategoryId in productForCreationDto.SubCategoriesId)
            {
                var selectedSubCategory = await _ctx.SubCategories.FirstOrDefaultAsync(sc => sc.Id == subCategoryId);
                var psc = new ProductSubCategory { Product = product, SubCategory = selectedSubCategory };
                product.SubCategories.Add(psc);
            }

            _ctx.Add(product);
            await _ctx.SaveChangesAsync();

            return Ok(_ctx.Products.FirstOrDefault(x => x.Name == productForCreationDto.Name));
        }


        [Authorize(Policy = "ModerateProductRole")]
        [HttpPost("edit-product/{id}")]
        public async Task<IActionResult> EditProduct(int id, ProductToEditDto productToEditDto)
        {

            var productFromDb = await _ctx.Products
                   .Include(l => l.Languages)
                   .Include(c => c.SubCategories)
                   .Include(r => r.Requirements)
                   .FirstOrDefaultAsync(p => p.Id == id);

            var selectedCategory = await _ctx.Categories.FirstOrDefaultAsync(c => c.Id == productToEditDto.CategoryId);


            var reqTest = _mapper.Map<Requirements>(productToEditDto.Requirements);
           

            var updatedProduct = new Product
            {
                Id = id,
                Name = productToEditDto.Name,
                Description = productToEditDto.Description,
                Pegi = productToEditDto.Pegi,
                Price = productToEditDto.Price,
                IsDigitalMedia = productToEditDto.IsDigitalMedia,
                ReleaseDate = productToEditDto.ReleaseDate,
                Category = selectedCategory,
                Requirements = reqTest,
                Languages = new List<ProductLanguage>(),
                Photos = new List<Photo>(),
                SubCategories = new List<ProductSubCategory>()

            };

            foreach (var languageId in productToEditDto.LanguagesId)
            {

                var selectedLanguages = await _ctx.Languages.FirstOrDefaultAsync(l => l.Id == languageId);

                var pl = new ProductLanguage { Product = updatedProduct, Language = selectedLanguages };
                if (!productFromDb.Languages.Contains(pl))
                {
                    updatedProduct.Languages.Add(pl);
                }

            }
            foreach (var photo in productToEditDto.Photos)
            {
                var photoToCreate = new Photo { Url = photo };
                updatedProduct.Photos.Add(photoToCreate);
            }

            foreach (var subCategoryId in productToEditDto.SubCategoriesId)
            {
                var selectedSubCategory = await _ctx.SubCategories.FirstOrDefaultAsync(sc => sc.Id == subCategoryId);
                var psc = new ProductSubCategory { Product = updatedProduct, SubCategory = selectedSubCategory };
                if (!productFromDb.SubCategories.Contains(psc))
                {
                    updatedProduct.SubCategories.Add(psc);
                }
                updatedProduct.SubCategories.Add(psc);
            }
            //var up = _mapper.Map<Product>(productToEditDto);




            productFromDb.Name = updatedProduct.Name;
            productFromDb.Description = updatedProduct.Description;
            productFromDb.Pegi = updatedProduct.Pegi;
            productFromDb.Price = updatedProduct.Price;
            productFromDb.IsDigitalMedia = updatedProduct.IsDigitalMedia;
            productFromDb.ReleaseDate = updatedProduct.ReleaseDate;
            productFromDb.Category = selectedCategory;
            if (updatedProduct.Languages != null)
            {
                productFromDb.Languages = updatedProduct.Languages;
            }
            productFromDb.Requirements = reqTest;
            if (updatedProduct.SubCategories != null)
            {
                productFromDb.SubCategories = updatedProduct.SubCategories;
            }
            productFromDb.Photos = updatedProduct.Photos;

            await _ctx.SaveChangesAsync();
            return Ok(await _ctx.Products.FindAsync(id));
        }

    }
}