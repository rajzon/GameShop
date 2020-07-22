using System.Collections.Generic;
using System.Linq;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static IQueryable<RequirementsForEditDto> GetRequirementsForProduct(this DbSet<Requirements> requirements, int id) 
        {
            var result = requirements.Where(r => r.ProductId == id)
                        .Select(requriements => new RequirementsForEditDto
                        {
                            OS = requriements.OS,
                            Processor = requriements.Processor,
                            RAM = requriements.RAM,
                            GraphicsCard = requriements.GraphicsCard,
                            HDD = requriements.HDD,
                            IsNetworkConnectionRequire = requriements.IsNetworkConnectionRequire
                        });

            return result;
        }

        public static IQueryable<ProductToEditDto> GetProductForEdit(this DbSet<Product> products, ApplicationDbContext ctx, RequirementsForEditDto requirements, IEnumerable<Photo> photosFromRepo, int id) 
        {
            var result = products.Where(x => x.Id == id)
                .Select(product => new ProductToEditDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    Pegi = product.Pegi,
                    Price = product.Price,
                    ReleaseDate = product.ReleaseDate,
                    IsDigitalMedia = product.IsDigitalMedia,
                    SubCategoriesId = (from productSubCategory in product.SubCategories
                                       join subCategory in ctx.SubCategories
                                       on productSubCategory.SubCategoryId
                                       equals subCategory.Id
                                       select subCategory.Id).ToList(),
                    LanguagesId = (from productLangauge in product.Languages
                                   join language in ctx.Languages
                                   on productLangauge.LanguageId
                                   equals language.Id
                                   select language.Id).ToList(),
                    Requirements = requirements,
                    CategoryId = ctx.Categories.FirstOrDefault(c => c.Id == EF.Property<int>(product, "CategoryId")).Id,
                    Photos = photosFromRepo.ToList()
                });

            return result;
        }
        
    }
}