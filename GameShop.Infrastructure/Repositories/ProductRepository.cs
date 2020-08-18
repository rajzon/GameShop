using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Helpers;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.Domain.Dtos.ProductDtos;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product> , IProductRepository
    {

        public ProductRepository(ApplicationDbContext ctx)
            :base(ctx)
        {            
        }

        public async Task<PagedList<ProductForModerationDto>> GetProductsForModerationAsync(ProductParams productParams)
        {
            var products = _ctx.Products.OrderBy(x => x.Id)
                .Select(product => new ProductForModerationDto
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
                });

            return await PagedList<ProductForModerationDto>.CreateAsync(products, productParams.PageNumber, productParams.PageSize);
        }

        public async Task<PagedList<ProductForSearchingDto>> GetProductsForSearchingAsync(ProductParams productParams)
        {
            var products =  _ctx.Products.Select(product => new ProductForSearchingDto
            {
                Id =  product.Id,
                Name = product.Name,
                Price = product.Price,
                Photo = product.Photos.Where(p => p.ProductId == product.Id).Select(p => new Photo {
                    Id = p.Id,
                    Url = p.Url,
                    DateAdded = p.DateAdded,
                    isMain = p.isMain
                } ).FirstOrDefault(p => p.isMain == true),
                CategoryName = _ctx.Categories.FirstOrDefault(c => c.Id == EF.Property<int>(product, "CategoryId")).Name
            });

            return await PagedList<ProductForSearchingDto>.CreateAsync(products, productParams.PageNumber, productParams.PageSize);
        }


        public async Task<Product> CreateAsync(ProductForCreationDto productForCreationDto, Requirements requirements, Category selectedCategory)
        {
            var product = new Product
            {
                Name = productForCreationDto.Name,
                Description = productForCreationDto.Description,
                Pegi = productForCreationDto.Pegi,
                Price = productForCreationDto.Price,
                IsDigitalMedia = productForCreationDto.IsDigitalMedia,
                ReleaseDate = productForCreationDto.ReleaseDate,
                Category = selectedCategory,
                Requirements = requirements,
                Languages = new List<ProductLanguage>(),
                SubCategories = new List<ProductSubCategory>()
            };

            foreach (var languageId in productForCreationDto.LanguagesId)
            {

                var selectedLanguages = await _ctx.Languages.FirstOrDefaultAsync(l => l.Id == languageId);
                var pl = new ProductLanguage { Product = product, Language = selectedLanguages };
                product.Languages.Add(pl);

            }

            foreach (var subCategoryId in productForCreationDto.SubCategoriesId)
            {
                var selectedSubCategory = await _ctx.SubCategories.FirstOrDefaultAsync(sc => sc.Id == subCategoryId);
                var psc = new ProductSubCategory { Product = product, SubCategory = selectedSubCategory };
                product.SubCategories.Add(psc);
            }

            return product;
        }

        public override async Task<Product> GetAsync(int id) 
        {
            return await _ctx.Products
                                .Include(l => l.Languages)
                                    .ThenInclude(productLanguage => productLanguage.Language)
                                .Include(p => p.Photos)
                                .Include(p => p.Category)
                                .Include(p => p.SubCategories)
                                    .ThenInclude(productSubCategory => productSubCategory.SubCategory)
                                .Include(p => p.Requirements)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

         public async Task<Product> EditAsync(int id, ProductEditDto productToEditDto, Requirements requirements, Category selectedCategory, Product productFromDb)
        {

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
                Requirements = requirements,
                Languages = new List<ProductLanguage>(),               
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

            foreach (var subCategoryId in productToEditDto.SubCategoriesId)
            {
                var selectedSubCategory = await _ctx.SubCategories.FirstOrDefaultAsync(sc => sc.Id == subCategoryId);
                var psc = new ProductSubCategory { Product = updatedProduct, SubCategory = selectedSubCategory };
                if (!productFromDb.SubCategories.Contains(psc))
                {
                    updatedProduct.SubCategories.Add(psc);
                }
            }

            return updatedProduct;
        }

        public async Task<Product> GetWithPhotosOnly(int productId)
        {
            var product = await _ctx.Products
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(x => x.Id == productId);

            return product;
        }

        public async Task<ProductToEditDto> GetProductToEditAsync(RequirementsForEditDto requirements, IEnumerable<Photo> photosFromRepo, int id)
        {
            var result = await _ctx.Products.Where(x => x.Id == id)
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
                    Photos = photosFromRepo.ToList()
                }).FirstOrDefaultAsync();

            return result;
        }
        
    }
}