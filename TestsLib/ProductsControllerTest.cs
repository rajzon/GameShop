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
    public class ProductsControllerTest : TestBase, IDisposable
    {
      

        [Theory]
        [InlineData(true,false,1)] 
        [InlineData(false,false,0)] 
        public  void UnitTest_Given_User_When_Register_Then_Return_BadRequest(bool isCreated , bool isRoleAdded, int addRoleCount) 
        {


            //Assert
            var createResult = IdentityResult.Failed();
            var addRoleResult = IdentityResult.Failed();

            if (isCreated)
            {
                createResult = IdentityResult.Success;
            }
            
            if (isRoleAdded)
            {
                addRoleResult = IdentityResult.Success;
            }

            var userToCreate = new UserForRegisterDto 
            {
                Username = "Example",
                Email = "takiTam@example.pl",
                Password = "Aladad"
            };

            string emailPattern =  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            _userManager.Setup(x=> x.CreateAsync(
                            It.Is<User>(u => u.UserName.Contains(userToCreate.Username) && 
                                        u.Email.Contains(userToCreate.Email) && u.UserName.Length > 0 && Regex.IsMatch(userToCreate.Email,emailPattern,RegexOptions.IgnoreCase)), 
                            It.Is<string>(p => p.Contains(userToCreate.Password) && (p.Length > 4 && p.Length < 8) )))
                            .Returns(Task.FromResult(createResult))
                            .Verifiable();
            _userManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.Is<string>(x => x.Contains("Customer")))).Returns(Task.FromResult(addRoleResult)).Verifiable();

            //var testtsts = signInManager.Object;
            
            var cut = new AuthController(_config.Object, _userManager.Object, _signInManager.Object);


           
            
            //Act
            var result = cut.Register(userToCreate);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsNotType<OkObjectResult>(result.Result);

            _userManager.Verify(v => v.CreateAsync(It.Is<User>(x => x.UserName == userToCreate.Username), It.Is<string>(x => x == userToCreate.Password)), Times.Once());
            _userManager.Verify(v => v.AddToRoleAsync(It.Is<User>(x => x.UserName == userToCreate.Username), It.Is<string>(x => x.Contains("Customer"))), Times.Exactly(addRoleCount));


            
            

        }

        [Fact]
        public void UnitTest_Given_User_When_Register_Then_Return_OK()
        {

            //Assert
            var userToCreate = new UserForRegisterDto 
            {
                Username = "Example",
                Email = "takiTam@example.pl",
                Password = "Aladad"
            };

            string emailPattern =  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            _userManager.Setup(x=> x.CreateAsync(
                            It.Is<User>(u => u.UserName.Contains(userToCreate.Username) && 
                                        u.Email.Contains(userToCreate.Email) && u.UserName.Length > 0 && Regex.IsMatch(userToCreate.Email,emailPattern,RegexOptions.IgnoreCase)), 
                            It.Is<string>(p => p.Contains(userToCreate.Password) && (p.Length > 4 && p.Length < 8) )))
                            .Returns(Task.FromResult(IdentityResult.Success))
                            .Verifiable();
            _userManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.Is<string>(x => x.Contains("Customer")))).Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            //var testtsts = signInManager.Object;
            
            var cut = new AuthController(_config.Object, _userManager.Object, _signInManager.Object);


           
            
            //Act
            var result = cut.Register(userToCreate);


            //Assert
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);

            _userManager.Verify(v => v.CreateAsync(It.Is<User>(x => x.UserName == userToCreate.Username), It.Is<string>(x => x == userToCreate.Password)), Times.Once);
           _userManager.Verify(v => v.AddToRoleAsync(It.Is<User>(x => x.UserName == userToCreate.Username), It.Is<string>(x => x.Contains("Customer"))), Times.Once);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(5)]
        public void Given_ProductId_When_GetProduct_Then_Return_Ok(int productId)
        {
            // Arrange
            var sut = new ProductsController(_mapper, _unitOfWork);

            var data = Data.Product();

            //act
            var result = (OkObjectResult)(sut.GetProduct(productId).Result);

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            result.Value.Should().BeOfType(typeof(Product));

            result.Value.Should().BeEquivalentTo(data.FirstOrDefault(d => d.Id == productId));


        }

        [Theory]
        [InlineData(100)]
        [InlineData(15)]
        [InlineData(8)]
        public void Given_ProductId_When_GetProduct_Then_Return_NotFound(int productId)
        {
            // Arrange
            var cut = new ProductsController(_mapper, _unitOfWork);

            //act
            var result = cut.GetProduct(productId).Result;

            //Assert
            Assert.IsType<NotFoundResult>(result);


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

            result.As<OkObjectResult>().Value.As<List<ProductForSearchingDto>>().FirstOrDefault()
                    .Should()
                    .BeEquivalentTo(expected.FirstOrDefault());

            result.As<OkObjectResult>().Value.As<List<ProductForSearchingDto>>().LastOrDefault()
                    .Should()
                    .BeEquivalentTo(expected.LastOrDefault());
                    
            result.As<OkObjectResult>().Value.As<List<ProductForSearchingDto>>().Take(4).ToList()
                    .Should()
                    .BeEquivalentTo(expected.Take(4).ToList());

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

            //Act
            var result = sut.GetProductsForSearching(productParams).Result;
        

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            result.As<OkObjectResult>().Value.As<List<ProductForSearchingDto>>()
                    .Should()
                    .BeEquivalentTo(expected);
            
            result.As<OkObjectResult>().Value.As<List<ProductForSearchingDto>>().Count
                    .Should()
                    .Be(expectedCount);

        }

    }

}
