using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameShop.Application.Helpers;
using GameShop.Application.Interfaces;
using GameShop.Application.Mappings;
using GameShop.Infrastructure.Extensions;
using GameShop.Domain.Dtos;
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
using Xunit;
using FluentAssertions;
using TestsLib.DataForTests;
using System.Linq;

namespace TestsLib
{
    //Info Not tested GetProduct() , case when return Ok with product
    public class ProductsControllerTest : TestBase, IDisposable
    {
      

        [Theory]
        [InlineData(100)]
        [InlineData(15)]
        [InlineData(8)]
        public void IntegrationTest_Given_ProductId_When_GetProduct_Then_Return_NotFound(int productId)
        {
            // Arrange
            var expected = Data.Product().Where(x => x.Id == productId).FirstOrDefault();
            var sut = new ProductsController(_mapper, _unitOfWork);

            //act
            var result = sut.GetProduct(productId).Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();


        }


        [Fact]
        public void IntegrationTest_Given_DefaultProductParams_When_GetProductsForSearching_Then_Returns_Ok_With_PagedProducts()
        {
            //Arrange

            var httpContext = new DefaultHttpContext();

            var expected =  Data.ProductForSearchingDto();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new ProductsController(_mapper, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            var productParams = new ProductParams();

            //Act
            var result = sut.GetProductsForSearching(productParams).Result;
        

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected);

        }

        [Theory]
        [InlineData(3,1,3)]
        [InlineData(1,1,1)]
        [InlineData(3,2,3)]
        [InlineData(5,2,2)]
        [InlineData(7,1,7)]
        [InlineData(7,2,0)]
        public void IntegrationTest_Given_ProductParamsFromQuery_When_GetProductsForSearching_Then_Returns_Ok_With_Products(int pageSize, int pageNumber, int expectedCount)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var data =  Data.ProductForSearchingDto();

            int totalItems = data.Count();

            var expected = data.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new ProductsController(_mapper, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            var productParams = new ProductParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var expectedPaginationHeader = JsonConvert.SerializeObject(new {currentPage = pageNumber, itemsPerPage = pageSize, totalItems = 7, totalPages = (int)Math.Ceiling(totalItems/ (double)pageSize)});

            //Act
            var result = sut.GetProductsForSearching(productParams).Result;
        

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            sut.HttpContext.Response.Headers["Pagination"].Should().BeEquivalentTo(expectedPaginationHeader);

            result.As<OkObjectResult>().Value.As<List<ProductForSearchingDto>>()
                    .Should()
                    .BeEquivalentTo(expected);
            
            result.As<OkObjectResult>().Value.As<List<ProductForSearchingDto>>().Count
                    .Should()
                    .Be(expectedCount);

        }

    }

}
