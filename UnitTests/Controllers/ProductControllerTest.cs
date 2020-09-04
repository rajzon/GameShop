using System.Collections.Generic;
using FluentAssertions;
using GameShop.Application.Helpers;
using GameShop.Domain.Dtos;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using UnitTests.DataForTests;
using Moq;
using System.Linq;
using GameShop.Application.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GameShop.Domain.Model;
using System;

namespace UnitTests.Controllers
{

    // Info: GetProductsForSearching third if statement not tested
    public class ProductControllerTest : UnitTestsBase, IDisposable
    {


        [Fact]
        public void Given_DefaultProductParamsFromQuery_When_GetProductsForSearching_ThenReturn_OKStatusWithProductForSearchingDtoAndPaginationHeader()
        {
            //Arrange
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext()
            };

            var defaultProductParams = new ProductParams();

            var productsForSearchingDto = DataForTests.Data.ProductForSearchingDto();

            int pageNumber = defaultProductParams.PageNumber;

            int pageSize = defaultProductParams.PageSize;

            int itemsCount = productsForSearchingDto.Count;

            var pagedList = new PagedList<ProductForSearchingDto>(productsForSearchingDto, itemsCount, pageNumber, pageSize);

            // var productRepoMock = new Mock<IProductRepository>();
            //     productRepoMock.Setup(s => s.GetProductsForSearchingAsync(defaultProductParams)).ReturnsAsync(pagedList);

            // _startup.mockedUnitOfWork.Setup(s => s.Product).Returns(productRepoMock.Object);

            _mockedUnitOfWork.Setup(s => s.Product.GetProductsForSearchingAsync(defaultProductParams)).Returns(Task.FromResult(pagedList));

            var expectedPaginationHeader = JsonConvert.SerializeObject(new {currentPage = pageNumber, itemsPerPage = pageSize, totalItems = 7, totalPages = 1});
            var cut = new ProductsController(_mapper, _mockedUnitOfWork.Object)
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = cut.GetProductsForSearching(defaultProductParams).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            cut.HttpContext.Response.Headers["Pagination"].Should().BeEquivalentTo(expectedPaginationHeader);
            
            result.As<OkObjectResult>().Value.Should().BeOfType<List<ProductForSearchingDto>>();

            result.As<OkObjectResult>().Value.As<List<ProductForSearchingDto>>()
                .Should().NotBeNull();

            _mockedUnitOfWork.Verify(v => v.Product.GetProductsForSearchingAsync(defaultProductParams), Times.Once);
            
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(0,1)]
        [InlineData(1,0)] 
        [InlineData(-1,-1)] 
        [InlineData(-1,1)]
        [InlineData(1,-1)]
        public void Given_ProductParamsFromQueryWithPageNumberAndPageSizeLessThen1_When_GetProductsForSearching_ThenReturn_BadRequestStatusWithMessage(int pageNumber, int pageSize)
        {
            //Arrange
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext()
            };

            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var cut = new ProductsController(_mapper, _mockedUnitOfWork.Object)
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = cut.GetProductsForSearching(productParams).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.As<string>()
                    .Should().Be("PageNumber or PageSize is less then 1");
        }

        [Fact]
        public void Given_DefaultProductParamsFromQueryOnlyForCallMethod_When_GetProductsForSearching_ThenReturn_NotFound_BecauseProductsNotFoundInDb()
        {
            //Arrange
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext()
            };

            var defaultProductParams = new ProductParams();

            _mockedUnitOfWork.Setup(s => s.Product.GetProductsForSearchingAsync(It.IsAny<ProductParams>()))
                    .Returns(Task.FromResult<PagedList<ProductForSearchingDto>>(null));

            var cut = new ProductsController(_mapper, _mockedUnitOfWork.Object)
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = cut.GetProductsForSearching(defaultProductParams).Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.Product.GetProductsForSearchingAsync(It.IsAny<ProductParams>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(7)]
        public void Given_ProductId_When_GetProduct_ThenReturn_OkStatus(int productId)
        {
            //Arrange
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext()
            };

            var defaultProductParams = new ProductParams();

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId)).ReturnsAsync(new Product());

            var cut = new ProductsController(_mapper, _mockedUnitOfWork.Object)
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = cut.GetProduct(productId).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<Product>();

            result.As<OkObjectResult>().Value.As<Product>()
                    .Should().NotBeNull();

            _mockedUnitOfWork.Verify(v => v.Product.GetAsync(productId), Times.Once);

        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(120)]
        public void Given_ProductIdThatNotExistInDb_When_GetProduct_ThenReturn_NotFound(int productId)
        {
            //Arrange
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext()
            };

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId))
                    .Returns(Task.FromResult<Product>(null));

            var cut = new ProductsController(_mapper, _mockedUnitOfWork.Object)
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = cut.GetProduct(productId).Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.Product.GetAsync(productId), Times.Once);
        }



    }
}