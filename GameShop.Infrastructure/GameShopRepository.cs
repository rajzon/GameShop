using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<IEnumerable<ProductForSearchingDto>> GetProductsForSearchingAsync()
        {
            var products = await _ctx.Products.Select(product => new ProductForSearchingDto
            {
                Name = product.Name,
                Price = product.Price,
                PhotosUrl = product.Photos.Where(p => p.ProductId == product.Id).Select(p => p.Url).ToArray(),
                CategoryName = _ctx.Categories.FirstOrDefault(c => c.Id == EF.Property<int>(product, "CategoryId")).Name
            }).ToListAsync();

            // var productToReturn = _mapper.Map<IEnumerable<ProductForSearchingDto>>(products);

            return products;
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

            return product;
        }

        public async Task<Product> EditProduct(int id, ProductToEditDto productToEditDto, Requirements requirements, Category selectedCategory, Product productFromDb)
        {

            //  productFromDb = await _ctx.Products
            //        .Include(l => l.Languages)
            //        .Include(c => c.SubCategories)
            //        .Include(r => r.Requirements)
            //        .FirstOrDefaultAsync(p => p.Id == id);


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

            if (productToEditDto.Photos != null)
            {
                foreach (var photo in productToEditDto.Photos)
                {
                    var photoToCreate = new Photo { Url = photo };
                    if (!productFromDb.Photos.Contains(photoToCreate))
                    {
                        updatedProduct.Photos.Add(photoToCreate);
                    }

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