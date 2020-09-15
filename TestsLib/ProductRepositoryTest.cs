using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using System.Reflection;
using GameShop.Application.Helpers;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.Infrastructure.Repositories;
using TestsLib.DataForTests;
using Xunit;
using System.Text.RegularExpressions;
using GameShop.Domain.Dtos.ProductDtos;

namespace TestsLib
{
    //Info: GetAsync not tested
    public class ProductRepositoryTest : TestBase
    {
        
        [Fact]
        public void IntegrationTest_Given_DefaultProductParams_When_GetProductsForModerationAsync_ThenReturn_PagedListOfProductForModerationDto()
        {   
            //Arrange
            var expected = Data.ProductForModerationDto();

            var defaultProductParams = new ProductParams();

            var sut = new ProductRepository(_context);


            //Act
            var result = sut.GetProductsForModerationAsync(defaultProductParams).Result;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<PagedList<ProductForModerationDto>>();
            result.Should().BeEquivalentTo(expected);

        }

        [Theory]
        [InlineData(3,1,3)]
        [InlineData(1,1,1)]
        [InlineData(3,2,3)]
        [InlineData(5,2,2)]
        [InlineData(7,1,7)]
        [InlineData(7,2,0)]
        public void IntegrationTest_Given_ProductParams_When_GetProductsForModerationAsync_ThenReturn_PagedListOfProductForModerationDto(int pageSize, int pageNumber, int expectedCount)
        {   
            //Arrange
            var expected = Data.ProductForModerationDto().Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var sut = new ProductRepository(_context);


            //Act
            var result = sut.GetProductsForModerationAsync(productParams).Result;

            //Assert
            result.Should().NotBeNull();

            result.Should().BeOfType<PagedList<ProductForModerationDto>>();

            result.Should().BeEquivalentTo(expected);

            result.Count().Should().Be(expectedCount);

        }
        [Theory]
        [InlineData(0,0)]
        [InlineData(-3,-1)]
        [InlineData(-1,-1)]
        [InlineData(-3,-2)]
        [InlineData(-5,-2)]
        [InlineData(-7,-1)]
        [InlineData(-7,-2)]
        public void IntegrationTest_Given_ProductParamsThatProductNumberAndPageSizeAreLessThen1_When_GetProductsForModerationAsync_ThenThrow_ArgumentException(int pageSize, int pageNumber)
        {   
            //Arrange
            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var sut = new ProductRepository(_context);


            //Act


            //Assert
            sut.Invoking(s => s.GetProductsForModerationAsync(productParams))
                    .Should()
                    .Throw<ArgumentException>()
                    .WithMessage("PageNumber and PageSize are less then one");               

        }

        [Theory]
        [InlineData(3,-1)]
        [InlineData(1,-1)]
        [InlineData(3,-2)]
        [InlineData(5,-2)]
        [InlineData(7,-1)]
        [InlineData(7,-2)]
        public void IntegrationTest_Given_ProductParamsThatPageNumberIsLessThen1_When_GetProductsForModerationAsync_ThenThrow_ArgumentException(int pageSize, int pageNumber)
        {   
            //Arrange
            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var sut = new ProductRepository(_context);


            //Act


            //Assert
            sut.Invoking(s => s.GetProductsForModerationAsync(productParams))
                    .Should()
                    .Throw<ArgumentException>()
                    .WithMessage("PageNumber is less then one");

        }

        [Theory]
        [InlineData(-3,1)]
        [InlineData(-1,1)]
        [InlineData(-3,2)]
        [InlineData(-5,2)]
        [InlineData(-7,1)]
        [InlineData(-7,2)]
        public void IntegrationTest_Given_ProductParamsThatPageSizeIsLessThen1_When_GetProductsForModerationAsync_ThenThrow_ArgumentException(int pageSize, int pageNumber)
        {   
            //Arrange
            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var sut = new ProductRepository(_context);


            //Act


            //Assert
            sut.Invoking(s => s.GetProductsForModerationAsync(productParams))
                    .Should()
                    .Throw<ArgumentException>()
                    .WithMessage("PageSize is less then one");

        }

        [Fact]
        public void IntegrationTest_Given_Null_When_GetProductsForModerationAsync_ThenThrow_NullReferenceException()
        {   
            //Arrange
            var sut = new ProductRepository(_context);


            //Act

            //Assert
            sut.Invoking(s => s.GetProductsForModerationAsync(null))
                .Should()
                .Throw<NullReferenceException>();

        }


        [Fact]
        public void IntegrationTest_Given_DefaultProductParams_When_GetProductsForSearchingAsync_ThenReturn_PagedListOfProductForSearchingDto()
        {   
            //Arrange
            var expected = Data.ProductForSearchingDto();

            var defaultProductParams = new ProductParams();

            var sut = new ProductRepository(_context);


            //Act
            var result = sut.GetProductsForSearchingAsync(defaultProductParams).Result;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<PagedList<ProductForSearchingDto>>();
            result.Should().BeEquivalentTo(expected);

        }

        [Theory]
        [InlineData(3,1,3)]
        [InlineData(1,1,1)]
        [InlineData(3,2,3)]
        [InlineData(5,2,2)]
        [InlineData(7,1,7)]
        [InlineData(7,2,0)]
        public void IntegrationTest_Given_ProductParams_When_GetProductsForSearchingAsync_ThenReturn_PagedListOfProductForSearchingDto(int pageSize, int pageNumber, int expectedCount)
        {   
            //Arrange
            var expected = Data.ProductForSearchingDto().Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var sut = new ProductRepository(_context);


            //Act
            var result = sut.GetProductsForSearchingAsync(productParams).Result;

            //Assert
            result.Should().NotBeNull();

            result.Should().BeOfType<PagedList<ProductForSearchingDto>>();

            result.Should().BeEquivalentTo(expected);

            result.Count().Should().Be(expectedCount);

        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(-3,-1)]
        [InlineData(-1,-1)]
        [InlineData(-3,-2)]
        [InlineData(-5,-2)]
        [InlineData(-7,-1)]
        [InlineData(-7,-2)]
        public void IntegrationTest_Given_ProductParamsThatProductNumberAndPageSizeAreLessThen1_When_GetProductsForSearchingAsync_ThenThrow_ArgumentException(int pageSize, int pageNumber)
        {   
            //Arrange
            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var sut = new ProductRepository(_context);


            //Act


            //Assert
            sut.Invoking(s => s.GetProductsForSearchingAsync(productParams))
                    .Should()
                    .Throw<ArgumentException>()
                    .WithMessage("PageNumber and PageSize are less then one");

        }

        [Theory]
        [InlineData(3,-1)]
        [InlineData(1,-1)]
        [InlineData(3,-2)]
        [InlineData(5,-2)]
        [InlineData(7,-1)]
        [InlineData(7,-2)]
        public void IntegrationTest_Given_ProductParamsThatPageNumberIsLessThen1_When_GetProductsForSearchingAsync_ThenThrow_ArgumentException(int pageSize, int pageNumber)
        {   
            //Arrange
            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var sut = new ProductRepository(_context);


            //Act


            //Assert
            sut.Invoking(s => s.GetProductsForSearchingAsync(productParams))
                    .Should()
                    .Throw<ArgumentException>()
                    .WithMessage("PageNumber is less then one");

        }

        [Theory]
        [InlineData(-3,1)]
        [InlineData(-1,1)]
        [InlineData(-3,2)]
        [InlineData(-5,2)]
        [InlineData(-7,1)]
        [InlineData(-7,2)]
        public void IntegrationTest_Given_ProductParamsThatPageSizeIsLessThen1_When_GetProductsForSearchingAsync_ThenThrow_ArgumentException(int pageSize, int pageNumber)
        {   
            //Arrange
            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var sut = new ProductRepository(_context);


            //Act


            //Assert
            sut.Invoking(s => s.GetProductsForSearchingAsync(productParams))
                    .Should()
                    .Throw<ArgumentException>()
                    .WithMessage("PageSize is less then one");

        }

        [Fact]
        public void IntegrationTest_Given_Null_When_GetProductsForSearchingAsync_ThenThrow_NullReferenceException()
        {   
            //Arrange
            var sut = new ProductRepository(_context);

            //Act

            //Assert
            sut.Invoking(s => s.GetProductsForSearchingAsync(null))
                .Should()
                .Throw<NullReferenceException>();

        }


        [Fact]
        public void IntegrationTest_Given_ProductForCreationDtoRequirementsAndCategory_When_ScaffoldProductForCreationAsync_ThenReturn_Product()
        {   
            //Arrange
            var productForCreationDto = new ProductForCreationDto()
            {
                Name = "Test Name",
                Description = "Test Description",
                Pegi = 12,
                Price = 55.02M,
                IsDigitalMedia = true,
                ReleaseDate = DateTime.Parse("2020-07-20"),
                CategoryId = 2,
                Requirements = new RequirementsForCreationDto()
                {
                    OS = "Test Windows 7/8/10",
                    Processor = "Test Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                    RAM = 6,
                    GraphicsCard = "Test NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                    HDD = 25,
                    IsNetworkConnectionRequire = true
                },
                LanguagesId = new int[] { 1,5,6},
                SubCategoriesId = new int[] {1,3},
            };

            var requirements = _mapper.Map<Requirements>(productForCreationDto.Requirements);

            var selectedCategory = _context.Categories.Where(c => c.Id == productForCreationDto.CategoryId).FirstOrDefault();

            var expected = Data.ExpectedProductForCreateAsync(productForCreationDto, requirements, selectedCategory);


            var sut = new ProductRepository(_context);


            //Act
            var result = sut.ScaffoldProductForCreationAsync(productForCreationDto, requirements, selectedCategory).Result;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(expected, option => option.IgnoringCyclicReferences());

        }

        [Fact]
        public void IntegrationTest_Given_ProductForCreationDtoWithoutCategoryLanguageSubCategoryRequirements_When_ScaffoldProductForCreationAsync_ThenReturn_Product()
        {   
            //Arrange
            var productForCreationDto = new ProductForCreationDto()
            {
                Name = "Test Name",
                Description = "Test Description",
                Pegi = 12,
                Price = 55.02M,
                IsDigitalMedia = true,
                ReleaseDate = DateTime.Parse("2020-07-20"),
            };

            var requirements = _mapper.Map<Requirements>(productForCreationDto.Requirements);

            var selectedCategory = _context.Categories.Where(c => c.Id == productForCreationDto.CategoryId).FirstOrDefault();

            var expected = new Product()
            {
                Name = productForCreationDto.Name,
                Description = productForCreationDto.Description,
                Pegi = productForCreationDto.Pegi,
                Price = productForCreationDto.Price,
                IsDigitalMedia = productForCreationDto.IsDigitalMedia,
                ReleaseDate = productForCreationDto.ReleaseDate,
                Languages = new List<ProductLanguage>(),
                SubCategories = new List<ProductSubCategory>(),
                Stock = new Stock()
            };


            var sut = new ProductRepository(_context);


            //Act
            var result = sut.ScaffoldProductForCreationAsync(productForCreationDto, requirements, selectedCategory).Result;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Product>();
            
            result.As<Product>().Category.Should().BeNull();

            result.As<Product>().SubCategories.Should().BeEmpty();

            result.As<Product>().Languages.Should().BeEmpty();

            result.Should().BeEquivalentTo(expected, option => option.IgnoringCyclicReferences());

        }

        [Fact]
        public void IntegrationTest_Given_ProductId_ProductEditDto_Requirements_Category_AndProductThatIsAlreadyInDbToEdit_When_ScaffoldProductForEditAsync_ThenReturn_Product()
        {   
            //Arrange
            var productId = 2;

            var productEditDto = new ProductEditDto()
            {
                Name = "Update Test Name",
                Description = "Update Test Description",
                Pegi = 12,
                Price = 55.02M,
                IsDigitalMedia = true,
                ReleaseDate = DateTime.Parse("2020-07-20"),
                CategoryId = 2,
                Requirements = new RequirementsForEditDto()
                {
                    OS = "Update Test Windows 7/8/10",
                    Processor = "Update Test Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                    RAM = 6,
                    GraphicsCard = "Update Test NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                    HDD = 25,
                    IsNetworkConnectionRequire = true
                },
                LanguagesId = new int[] {1,5,6},
                SubCategoriesId = new int[] {1,3},
            };

            var requirements = _mapper.Map<Requirements>(productEditDto.Requirements);

            var selectedCategory = _context.Categories.Where(c => c.Id == productEditDto.CategoryId).FirstOrDefault();

            var expected = Data.ExpectedProductForEditAsync(productId, productEditDto, requirements, selectedCategory);

            var productFromDbToUpdate = _unitOfWork.Product.GetAsync(productId).Result;


            var sut = new ProductRepository(_context);


            //Act
            var result = sut.ScaffoldProductForEditAsync(productId, productEditDto, requirements, selectedCategory, productFromDbToUpdate).Result;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(expected, option => 
                    option.IgnoringCyclicReferences()
                          .Excluding(x => Regex.IsMatch(x.SelectedMemberPath, @"SubCategories\[\d+\]\.SubCategory.Product")));
            
            result.Should().NotBeEquivalentTo(productFromDbToUpdate);


        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(7)]
        public void IntegrationTest_Given_productId_When_GetWithPhotosOnly_ThenReturn_Product(int productId)
        {
            //Arrange
            var expected = Data.ExpectedProductForGetWithPhotosOnly().FirstOrDefault(p => p.Id == productId);

            var sut = new ProductRepository(_context);

            //Act
            var result = sut.GetWithPhotosOnly(productId).Result;


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(expected, 
                        option => option.Excluding((x => 
                                    Regex.IsMatch(x.SelectedMemberPath, @"Photos\[\d+\]\.Product"))));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(7)]
        public void IntegrationTest_Given_productId_When_GetProductToEditAsync_ThenReturn_ProductToEdit(int productId)
        {
            //Arrange
            var expected = Data.ProductToEditDto().Skip(productId - 1).FirstOrDefault();

            var requirements = Data.RequirementsForEditDto().Skip(productId - 1).FirstOrDefault();

            var photosForProduct = Data.Photo().Where(x => x.ProductId == productId).ToList();

            var sut = new ProductRepository(_context);

            //Act
            var result = sut.GetProductToEditAsync(requirements, photosForProduct, productId).Result;


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductToEditDto>();
            result.Should().BeEquivalentTo(expected);
        }


    }
}