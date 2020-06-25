using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameShop.Application.Helpers;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure
{
    public class GameShopRepository : IGameShopRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _mapper;

        public GameShopRepository(ApplicationDbContext ctx, IMapper mapper)
        {
            _mapper = mapper;
            _ctx = ctx;
        }

        public void Add<T>(T entity) where T : class
        {
            _ctx.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _ctx.Remove(entity);
        }

        public async Task<Product> GetProductWithPhotos(int productId)
        {
            var product = await _ctx.Products
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(x => x.Id == productId);

            return product;
        }

        public async Task<Product> GetProductForDelete(int productId)
        {
            var selectedProduct = await _ctx.Products
                   .Include(l => l.Languages)
                   .Include(c => c.SubCategories)
                   .Include(r => r.Requirements)
                   .Include(p => p.Photos)
                   .FirstOrDefaultAsync(p => p.Id == productId);

            return selectedProduct;
        }

        public async Task<Product> GetProductForEdit(int productId)
        {
            var selectedProduct = await _ctx.Products
                   .Include(c => c.SubCategories)
                   .Include(r => r.Requirements)
                   .Include(l => l.Languages)
                   .Include(c => c.Category)
                   .FirstOrDefaultAsync(p => p.Id == productId);

            return selectedProduct;
        }

        public async Task<Photo> GetMainPhotoForProduct(int productId)
        {
           return await _ctx.Photos.Where(p => p.ProductId == productId).FirstOrDefaultAsync(p => p.isMain);
        }

        public async Task<Photo> GetPhoto(int photoId)
        {
            var photo = await _ctx.Photos.FirstOrDefaultAsync(x => x.Id == photoId);

            return photo;
        }

        public async Task<ICollection<Photo>> GetPhotosForProduct(int productId)
        {
            var photo = await _ctx.Photos.Where(p => p.ProductId == productId).Select(p => new Photo {
                        Id = p.Id,
                        Url = p.Url,
                        DateAdded = p.DateAdded,
                        isMain = p.isMain
                    }).ToListAsync();

            return photo;
        }

        public async Task<PagedList<ProductForSearchingDto>> GetProductsForSearchingAsync(ProductParams productParams)
        {
            var products =  _ctx.Products.Select(product => new ProductForSearchingDto
            {
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

        public async Task<Category> GetCategory(int categoryId)
        {
            var category = await _ctx.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

            return category;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            // var categoriesToRetrun = new List<Category>();
            // var categories = await _ctx.Categories.OrderBy(x => x.Id)
            //             .Select(category => new 
            //             {   
            //                 Id = category.Id,
            //                 Name = category.Name,
            //                 Description = category.Description
            //             }).ToListAsync();

            // for (int i = 0; i < categories.Count; i++)
            // {
            //     var category = new Category
            //     {
            //         Id = categories.ElementAt(i).Id,
            //         Name = categories.ElementAt(i).Name,
            //         Description = categories.ElementAt(i).Description
            //     };
            //     categoriesToRetrun.Add(category);
            // }
            // return categoriesToRetrun;
            var categories = await _ctx.Categories.OrderBy(x => x.Id).ToListAsync();

            return categories;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategories()
        {
            var subCategories = await _ctx.SubCategories.OrderBy(x => x.Id).ToListAsync();

            return subCategories;

        }

        public async Task<IEnumerable<Language>> GetLanguages()
        {
            var languages = await _ctx.Languages.OrderBy(x => x.Id).ToListAsync();

            return languages;

        }

        public async Task<Product> CreateProduct(ProductForCreationDto productForCreationDto, Requirements requirements, Category selectedCategory)
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
            _ctx.Add(product);
            await _ctx.SaveChangesAsync();

            return product;
        }

        public async Task<Product> EditProduct(int id, ProductToEditDto productToEditDto, Requirements requirements, Category selectedCategory, Product productFromDb)
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
                updatedProduct.SubCategories.Add(psc);
            }

            return updatedProduct;
        }

        // private async Task AddItem<TValue,E>(List<TValue> entityFieldCollection, E entity , int[] ids) where E : class where TValue : class
        // {
        //     if (entityFieldCollection != null)
        //     {
        //         var dataSet = _ctx.Set<E>().Find(entity.Id);
        //         var nestedDataSet = _ctx.Set<TValue>();
        //         foreach (var item in entityFieldCollection)
        //         {
        //             var sel = await nestedDataSet.FirstOrDefaultAsync(x => x.)
        //         }

        //     }
        // }

        public async Task<User> GetUser(int id)
        {
            //Later on I am gonna add Addreses Table so probably I may want to Include Address Table here
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            //Later on I am gonna add Addreses Table so probably I may want to Include Address Table here
            var users = await _ctx.Users.ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        
    }
}