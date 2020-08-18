using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GameShop.Application.Helpers;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GameShop.Extensions.Infrastructure
{
    public static class ModelBuilderExtensions
    {

        public static void SeedProducts(this ModelBuilder modelBuilder, IConfiguration appSettings)
        {
            var seedDataLocationOptions = appSettings.GetSection(SeedDataLocationOptions.SeedDataLocation).Get<SeedDataLocationOptions>();
            string productSeedDataLocation = seedDataLocationOptions.ProductSeedData;


            var currDirectory = Directory.GetCurrentDirectory();

            string productsData;
            List<Product> products = new List<Product>();
            if (!currDirectory.Contains("GameShop.UI"))
            {
                var testProjectDirectory = Directory.GetParent(currDirectory).Parent.Parent.Parent.FullName;
                var combined = Path.GetFullPath(Path.Combine(testProjectDirectory,productSeedDataLocation));
                productsData = System.IO.File.ReadAllText(combined);
                products = JsonConvert.DeserializeObject<List<Product>>(productsData);
            } 
            else 
            {
                productsData = System.IO.File.ReadAllText("../"+productSeedDataLocation);
                products = JsonConvert.DeserializeObject<List<Product>>(productsData);
            }
            

            

            

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

            string subCategorySeedDataLocation = seedDataLocationOptions.SubCategorySeedData;
            string subCategoriesData;
            List<SubCategory> subCategories = new List<SubCategory>();
            if (!currDirectory.Contains("GameShop.UI"))
            {
                var testProjectDirectory = Directory.GetParent(currDirectory).Parent.Parent.Parent.FullName;
                var combined = Path.GetFullPath(Path.Combine(testProjectDirectory,subCategorySeedDataLocation));
                subCategoriesData = System.IO.File.ReadAllText(combined);
                subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(subCategoriesData);
            } 
            else 
            {
                subCategoriesData = System.IO.File.ReadAllText("../"+subCategorySeedDataLocation);
                subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(subCategoriesData);
            }

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


            string requirementsSeedDataLocation = seedDataLocationOptions.RequirementsSeedData;
            string requirementsData;
            List<Requirements> requirements = new List<Requirements>();
            if (!currDirectory.Contains("GameShop.UI"))
            {
                var testProjectDirectory = Directory.GetParent(currDirectory).Parent.Parent.Parent.FullName;
                var combined = Path.GetFullPath(Path.Combine(testProjectDirectory,requirementsSeedDataLocation));
                requirementsData = System.IO.File.ReadAllText(combined);
                requirements = JsonConvert.DeserializeObject<List<Requirements>>(requirementsData);
            } 
            else 
            {
                requirementsData = System.IO.File.ReadAllText("../"+requirementsSeedDataLocation);
                requirements = JsonConvert.DeserializeObject<List<Requirements>>(requirementsData);
            }
            modelBuilder.Entity<Requirements>().HasData(requirements);




            string photoSeedDataLocation = seedDataLocationOptions.PhotoSeedData;
            string photosData;
            List<Photo> photos = new List<Photo>();
            if (!currDirectory.Contains("GameShop.UI"))
            {
                var testProjectDirectory = Directory.GetParent(currDirectory).Parent.Parent.Parent.FullName;
                var combined = Path.GetFullPath(Path.Combine(testProjectDirectory,photoSeedDataLocation));
                photosData = System.IO.File.ReadAllText(combined);
                photos = JsonConvert.DeserializeObject<List<Photo>>(photosData);
            } 
            else 
            {
                photosData = System.IO.File.ReadAllText("../"+photoSeedDataLocation);
                photos = JsonConvert.DeserializeObject<List<Photo>>(photosData);
            }
            modelBuilder.Entity<Photo>().HasData(photos);
        }



        //TO DO
        public static List<T> GetSeedDataOf<T>()
        {
            return new List<T>();
        }
    }
}