using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using GameShop.Application.Helpers;
using GameShop.Application.Mappings;
using GameShop.Domain.Dtos;
using GameShop.Domain.Dtos.ProductDtos;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using TestsLib.DataForTests;
using Xunit;

namespace TestsLib
{
    // TO DO: add tests for GetProductsForStockModeration()
    public class AdminControllerTest : TestBase, IDisposable
    {

        [Fact]
        public void IntegrationTest_Given_DefaultProductParams_When_GetProductsForModeration_Then_Return_PagedProducts_WithOkStatus_And_AddDefaultPaginationHeader()
        {
            //Arrange

            var httpContext = new DefaultHttpContext();

            var expected =  Data.ProductForModerationDto();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            var productParams = new ProductParams();

            //Act
            var result = sut.GetProductsForModeration(productParams).Result;

            var header =  sut.Response.Headers["Pagination"];

            var paginationHeader = JsonConvert.DeserializeObject<PaginationHeader>(header.ToString());
      

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected);

            result.As<OkObjectResult>().Value.As<List<ProductForModerationDto>>().FirstOrDefault()
                    .Should()
                    .BeEquivalentTo(expected.FirstOrDefault());

            result.As<OkObjectResult>().Value.As<List<ProductForModerationDto>>().LastOrDefault()
                    .Should()
                    .BeEquivalentTo(expected.LastOrDefault());
                    
            result.As<OkObjectResult>().Value.As<List<ProductForModerationDto>>().Take(4).ToList()
                    .Should()
                    .BeEquivalentTo(expected.Take(4).ToList());

            paginationHeader.Should().NotBeNull();
            paginationHeader.CurrentPage.Should().Be(1);
            paginationHeader.ItemsPerPage.Should().Be(10);

            
        }


        [Theory]
        [InlineData(3,1,3)]
        [InlineData(1,1,1)]
        [InlineData(3,2,3)]
        [InlineData(5,2,2)]
        [InlineData(7,1,7)]
        [InlineData(7,2,0)]
        public void IntegrationTest_Given_ProductParamsFromQuery_When_GetProductsForModeration_Then_Returns_PagedProducts_WithOkStatus_And_AddSpecificPaginationHeader(int pageSize, int pageNumber, int expectedCount)
        {
            //Arrange
            var httpContext = new DefaultHttpContext();

            var data =  Data.ProductForModerationDto();

            var expected = data.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            var productParams = new ProductParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            //Act
            var result = sut.GetProductsForModeration(productParams).Result;

            var header =  sut.Response.Headers["Pagination"];

            var paginationHeader = JsonConvert.DeserializeObject<PaginationHeader>(header.ToString());

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            result.As<OkObjectResult>().Value.As<List<ProductForModerationDto>>()
                    .Should()
                    .BeEquivalentTo(expected);
            
            result.As<OkObjectResult>().Value.As<List<ProductForModerationDto>>().Count
                    .Should()
                    .Be(expectedCount);

           paginationHeader.Should().NotBeNull();
           paginationHeader.CurrentPage.Should().Be(pageNumber);
           paginationHeader.ItemsPerPage.Should().Be(pageSize);
                             
            
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(7)]
        public void IntegrationTest_Given_ProductId_When_GetProductForEdit_Then_Return_ProductToEditDto_WithOkStatus(int productId)
        {
            //Arrange

            var httpContext = new DefaultHttpContext();

            var expected =  Data.ProductToEditDto();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.GetProductForEdit(productId).Result;
      

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            result.As<OkObjectResult>().Value.As<ProductToEditDto>().Should().BeEquivalentTo(expected.Skip(productId - 1).Take(1).FirstOrDefault());
                
            
        }

        [Fact]
        public void IntegrationTest_Given_ProductForCreationDto_When_CreateProduct_Then_Return_RouteToCreatedProductWithId8_WithCreatedStatus()
        {
            //Arrange 
            int numberOfProductsBeforeAct = 7;

            var httpContext = new DefaultHttpContext();

            var expected =  new ProductForCreationDto()
                {
                    Name = "TEST GAME",
                    Description = "Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.\r\n",
                    Pegi = 18,
                    Price = 50.82M,
                    IsDigitalMedia = false,
                    ReleaseDate = DateTime.Parse("2020-03-31"),
                    CategoryId = 2,
                    Requirements = new RequirementsForCreationDto()
                    {
                        OS = "Windows 7/8/10",
                        Processor = "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                        RAM = 3,
                        GraphicsCard = "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                        HDD = 25,
                        IsNetworkConnectionRequire = true
                    },
                    LanguagesId = new int[]
                    {
                        1,2,4
                    },
                    SubCategoriesId = new int[]
                    {
                        1,2,3,5
                    }
                };

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.CreateProduct(expected).Result;
      

            //Assert
            result.Should().BeOfType(typeof(CreatedAtRouteResult));

            result.As<CreatedAtRouteResult>().Value.Should().BeOfType(typeof(Product));

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Should()
                    .BeEquivalentTo(expected, options => options.ExcludingMissingMembers());

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Category.Id.Should().Be(expected.CategoryId);

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Languages.Select(l => l.LanguageId)
                    .ToList()
                    .Should()
                    .BeEquivalentTo(expected.LanguagesId);

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .SubCategories.Select(sc => sc.SubCategoryId)
                    .ToList()
                    .Should()
                    .BeEquivalentTo(expected.SubCategoriesId);

            result.As<CreatedAtRouteResult>().RouteValues.Values.FirstOrDefault().Should().Be(numberOfProductsBeforeAct + 1);
            result.As<CreatedAtRouteResult>().RouteName.Should().Be("GetProduct");   
            
        }

        [Fact]
        public void IntegrationTest_Given_ProductForCreationDtoWithoutLanguageSubCategoryRequirements_When_CreateProduct_Then_Return_RouteToCreatedProduct_WithCreatedStatus()
        {
            //Arrange 
            int numberOfProductsBeforeAct = 7;

            var httpContext = new DefaultHttpContext();

            var expected =  new ProductForCreationDto()
                {
                    Name = "TEST GAME",
                    Description = "Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.\r\n",
                    Pegi = 18,
                    Price = 50.82M,
                    IsDigitalMedia = false,
                    CategoryId = 1,
                    ReleaseDate = DateTime.Parse("2020-03-31"),
                };

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.CreateProduct(expected).Result;
      

            //Assert
            result.Should().BeOfType(typeof(CreatedAtRouteResult));

            result.As<CreatedAtRouteResult>().Value.Should().BeOfType(typeof(Product));

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Should()
                    .BeEquivalentTo(expected, options => options.ExcludingMissingMembers());

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Category.Should().NotBeNull();

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Languages.Should().BeEmpty();

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .SubCategories.Should().BeEmpty();

            result.As<CreatedAtRouteResult>().RouteValues.Values.FirstOrDefault().Should().Be(numberOfProductsBeforeAct + 1);
            result.As<CreatedAtRouteResult>().RouteName.Should().Be("GetProduct");   
            
        }

        [Fact]
        public void IntegrationTest_Given_ProductForCreationDtoWithoutLanguageSubCategoryRequirements_When_CreateProduct_CheckIfStockWithQuantity0WasCreated()
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var expected =  new ProductForCreationDto()
                {
                    Name = "TEST GAME",
                    Description = "Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.\r\n",
                    Pegi = 18,
                    Price = 50.82M,
                    IsDigitalMedia = false,
                    CategoryId = 1,
                    ReleaseDate = DateTime.Parse("2020-03-31"),
                };

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.CreateProduct(expected).Result;
      

            //Assert
            result.Should().BeOfType(typeof(CreatedAtRouteResult));

            result.As<CreatedAtRouteResult>().Value.As<Product>().Stock.Should().NotBeNull();

            result.As<CreatedAtRouteResult>().Value.As<Product>().Stock.Quantity.Should().Be(0);
            
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(7)]
        public void IntegrationTest_Given_ProductId_And_ProductToEditDto_When_EditProduct_Then_Return_RouteToEditedProduct_WithCreatedStatus(int productId)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var expected =  new ProductEditDto()
                {
                    Name = "TEST GAME",
                    Description = "Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.\r\n",
                    Pegi = 18,
                    Price = 50.82M,
                    IsDigitalMedia = false,
                    ReleaseDate = DateTime.Parse("2020-03-31"),
                    CategoryId = 2,
                    Requirements = new RequirementsForEditDto()
                    {
                        OS = "Windows 7/8/10",
                        Processor = "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                        RAM = 3,
                        GraphicsCard = "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                        HDD = 25,
                        IsNetworkConnectionRequire = true
                    },
                    LanguagesId = new int[]
                    {
                        1,2,4
                    },
                    SubCategoriesId = new int[]
                    {
                        1,2,3,5
                    }
                };

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.EditProduct(productId, expected).Result;
      

            //Assert
            result.Should().BeOfType(typeof(CreatedAtRouteResult));

            result.As<CreatedAtRouteResult>().Value.Should().BeOfType(typeof(Product));

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Should()
                    .BeEquivalentTo(expected, options => options.ExcludingMissingMembers());

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Category.Id.Should().Be(expected.CategoryId);

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .Languages.Select(l => l.LanguageId)
                    .ToList()
                    .Should()
                    .BeEquivalentTo(expected.LanguagesId);

            result.As<CreatedAtRouteResult>().Value.As<Product>()
                    .SubCategories.Select(sc => sc.SubCategoryId)
                    .ToList()
                    .Should()
                    .BeEquivalentTo(expected.SubCategoriesId);

            result.As<CreatedAtRouteResult>().RouteName.Should().Be("GetProduct");   
            
        }

        //Missing testing if Photo from Cloud storage was deleted
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(7)]
        public void IntegrationTest_Given_ProductId_When_DeleteProduct_Then_Return_NoContentStatus_And_DeleteProductWithPhotoFromDb(int productId)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.DeleteProduct(productId).Result;
            var deletedProduct = _unitOfWork.Product.GetAsync(productId).Result;
            var photosToBeDeleted = _unitOfWork.Photo.GetPhotosForProduct(productId).Result;
      

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
            deletedProduct.Should().BeNull();
            photosToBeDeleted.Should().BeEmpty();         
        }

        [Fact]
        public void IntegrationTest_Given_None_When_GetCategories_Then_Return_OkStatusWithCategoriesToReturnDto()
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var expected = Data.CategoryToReturnDto();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.GetCategories().Result;
      

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
            result.As<OkObjectResult>().Value.As<List<CategoryToReturnDto>>()
                .Should()
                .BeEquivalentTo(expected);        
        }

        [Fact]
        public void IntegrationTest_Given_None_When_GetSubCategories_Then_Return_OkStatusWithSubCategoriesToReturnDto()
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var expected = Data.SubCategoryToReturnDto();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.GetSubCategories().Result;
      

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
            result.As<OkObjectResult>().Value.As<List<SubCategoryToReturnDto>>()
                .Should()
                .BeEquivalentTo(expected);        
        }


        [Fact]
        public void IntegrationTest_Given_None_When_GetLanguages_Then_Return_OkStatusWithLanguageToReturnDto()
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var expected = Data.LanguageToReturnDto();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.GetLanguages().Result;
      

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
            result.As<OkObjectResult>().Value.As<List<LanguageToReturnDto>>()
                .Should()
                .BeEquivalentTo(expected);        
        }

         [Theory]
         [InlineData(1)]
         [InlineData(2)]
         [InlineData(3)]
        public void IntegrationTest_Given_CategoryId_When_GetCategory_Then_Return_OkStatusWithCategoryToReturnDto(int categoryId)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var expected = Data.CategoryToReturnDto();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.GetCategory(categoryId).Result;
      

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
            result.As<OkObjectResult>().Value.As<CategoryToReturnDto>()
                .Should()
                .BeEquivalentTo(expected.First(c => c.Id == categoryId));    
        }

        [Theory]
        [InlineData(1, 100)]
        [InlineData(2, 10)]
        [InlineData(7, 0)]
        public void IntegrationTest_Given_ProductIdAndQuantity_When_EditStockForProduct_Then_Return_OkStatusWithStockThatWasEdited(int productId, int quantity)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new AdminController(_mockedUserManager.Object, _mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.EditStockForProduct(productId, quantity).Result;
      

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            result.As<OkObjectResult>().Value.As<Stock>().Product.Should().NotBeNull();

            result.As<OkObjectResult>().Value.As<Stock>().ProductId.Should().Be(productId); 

            result.As<OkObjectResult>().Value.As<Stock>().Quantity.Should().Be(quantity); 
        }

    }
}