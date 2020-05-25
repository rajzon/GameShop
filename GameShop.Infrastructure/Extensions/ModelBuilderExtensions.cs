using System.Collections.Generic;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GameShop.Extensions.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void SeedProducts(this ModelBuilder modelBuilder)
        {

            var productsData = System.IO.File.ReadAllText("../GameShop.Infrastructure/SeedDataSource/ProductSeedData.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

            modelBuilder.Entity<Category>().HasData(
                new Category {Id=1, Name="PC", Description="PC Description"},
                new Category {Id=2, Name="PS4", Description="PS4 Description"},
                new Category {Id=3, Name="XONE", Description="XBOX One Description"}

            );
            

            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Language>().HasData(
                new Language {Id=1, Name= "Polish"},
                new Language {Id=2, Name= "English"},
                new Language {Id=3, Name= "German"},
                new Language {Id=4, Name= "Russian"},
                new Language {Id=5, Name= "French"},
                new Language {Id=6, Name= "Italian"}
            );
            modelBuilder.Entity<ProductLanguage>().HasData(
                new ProductLanguage {ProductId=1, LanguageId=1},
                new ProductLanguage {ProductId=1, LanguageId=2},
                new ProductLanguage {ProductId=1, LanguageId=5},
                new ProductLanguage {ProductId=2, LanguageId=2},
                new ProductLanguage {ProductId=2, LanguageId=4},
                new ProductLanguage {ProductId=3, LanguageId=2},
                new ProductLanguage {ProductId=4, LanguageId=1},
                new ProductLanguage {ProductId=4, LanguageId=2},
                new ProductLanguage {ProductId=4, LanguageId=3},
                new ProductLanguage {ProductId=4, LanguageId=4},
                new ProductLanguage {ProductId=4, LanguageId=5},
                new ProductLanguage {ProductId=4, LanguageId=6},
                new ProductLanguage {ProductId=5, LanguageId=2},
                new ProductLanguage {ProductId=5, LanguageId=3},
                new ProductLanguage {ProductId=6, LanguageId=1},
                new ProductLanguage {ProductId=6, LanguageId=3},
                new ProductLanguage {ProductId=7, LanguageId=4}
            );

            modelBuilder.Entity<ProductSubCategory>().HasData(
                new ProductSubCategory {ProductId=1, SubCategoryId=1},
                new ProductSubCategory {ProductId=2, SubCategoryId=1},
                new ProductSubCategory {ProductId=3, SubCategoryId=2},
                new ProductSubCategory {ProductId=4, SubCategoryId=3},
                new ProductSubCategory {ProductId=5, SubCategoryId=1},
                new ProductSubCategory {ProductId=5, SubCategoryId=7},
                new ProductSubCategory {ProductId=6, SubCategoryId=6},
                new ProductSubCategory {ProductId=7, SubCategoryId=5}

            );

            var subCategoriesData = System.IO.File.ReadAllText("../GameShop.Infrastructure/SeedDataSource/SubCategorySeedData.json");
            var subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(subCategoriesData);
            modelBuilder.Entity<SubCategory>().HasData(subCategories);

            modelBuilder.Entity<CategorySubCategory>().HasData(
                new CategorySubCategory {CategoryId=1, SubCategoryId=1},
                new CategorySubCategory {CategoryId=1, SubCategoryId=2},
                new CategorySubCategory {CategoryId=1, SubCategoryId=3},
                new CategorySubCategory {CategoryId=1, SubCategoryId=4},
                new CategorySubCategory {CategoryId=1, SubCategoryId=5},
                new CategorySubCategory {CategoryId=1, SubCategoryId=6},
                new CategorySubCategory {CategoryId=1, SubCategoryId=7},
                new CategorySubCategory {CategoryId=2, SubCategoryId=1},
                new CategorySubCategory {CategoryId=2, SubCategoryId=2},
                new CategorySubCategory {CategoryId=2, SubCategoryId=3},
                new CategorySubCategory {CategoryId=2, SubCategoryId=6},
                new CategorySubCategory {CategoryId=2, SubCategoryId=7},
                new CategorySubCategory {CategoryId=3, SubCategoryId=1},
                new CategorySubCategory {CategoryId=3, SubCategoryId=2},
                new CategorySubCategory {CategoryId=3, SubCategoryId=3},
                new CategorySubCategory {CategoryId=3, SubCategoryId=6},
                new CategorySubCategory {CategoryId=3, SubCategoryId=7}
            );
        

            var requirementsData = System.IO.File.ReadAllText("../GameShop.Infrastructure/SeedDataSource/RequirementsSeedData.json");
            var requirements = JsonConvert.DeserializeObject<List<Requirements>>(requirementsData);
            modelBuilder.Entity<Requirements>().HasData(requirements);

            var photosData = System.IO.File.ReadAllText("../GameShop.Infrastructure/SeedDataSource/PhotoSeedData.json");
            var photos = JsonConvert.DeserializeObject<List<Photo>>(photosData);
            modelBuilder.Entity<Photo>().HasData(photos);
        }
    }
}