using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using GameShop.Infrastructure.Interfaces;
using GameShop.UI;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Xunit;

namespace TestsLib
{
    public class AuthControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        
        public AuthControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;

            _client = _factory.CreateClient();
            // Seeding Users with Identity
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                Seed.SeedUsers(userManager, roleManager, config);
            }

        }

        [Fact]
        public void IntegrationTest_Given_UserForRegisterDto_When_Register_Then_Return_OkStatus()
        {
            //Arrange
            // var a = _unitOfWork.User.GetAsync(1);
            

            var expected = new UserForRegisterDto()
            {
                Username = "David",
                Email = "david.example@example.com",
                Password = "Password"
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/register", content).Result;

            User latestUser;
            using (var scope = _factory.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                latestUser = unitOfWork.User.GetLatestAsync().Result;
            } 
           

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.OK);
            latestUser.UserName.Should().Be(expected.Username);

        }

        [Fact]
        public void IntegrationTest_Given_UserForRegisterDtoWithUserNameThatContainsSpaces_When_Register_Then_Return_OkStatus()
        {
            //Arrange           
            var expected = new UserForRegisterDto()
            {
                Username = "     ",
                Email = "david.example@example.com",
                Password = "Password"
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/register", content).Result;

            User latestUser;
            using (var scope = _factory.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                latestUser = unitOfWork.User.GetLatestAsync().Result;
            } 
           

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            latestUser.UserName.Should().NotBe(expected.Username);

        }

        [Fact]
        public void IntegrationTest_Given_UserForRegisterDtoWithEmailThatContainsSpaces_When_Register_Then_Return_BadRequestStatus()
        {
            //Arrange           
            var expected = new UserForRegisterDto()
            {
                Username = "dddd",
                Email = "    ",
                Password = "Password"
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/register", content).Result;

            User latestUser;
            using (var scope = _factory.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                latestUser = unitOfWork.User.GetLatestAsync().Result;
            } 
           

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            latestUser.UserName.Should().NotBe(expected.Username);

        }


         [Fact]
        public void IntegrationTest_Given_UserForRegisterDtoWithPasswordThatContainsSpaces_When_Register_Then_Return_BadRequestStatus()
        {
            //Arrange           
            var expected = new UserForRegisterDto()
            {
                Username = "dddd",
                Email = "david.example@example.com",
                Password = "   "
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/register", content).Result;

            User latestUser;
            using (var scope = _factory.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                latestUser = unitOfWork.User.GetLatestAsync().Result;
            } 
           

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            latestUser.UserName.Should().NotBe(expected.Username);

        }


        [Fact]
        public void IntegrationTest_Given_UserForRegisterDtoWithPasswordThatHasLessThen4Characters_When_Register_Then_Return_BadRequestStatus()
        {
            //Arrange           
            var expected = new UserForRegisterDto()
            {
                Username = "dddd",
                Email = "david.example@example.com",
                Password = "pas"
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/register", content).Result;

            User latestUser;
            using (var scope = _factory.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                latestUser = unitOfWork.User.GetLatestAsync().Result;
            } 
           

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            latestUser.UserName.Should().NotBe(expected.Username);

        }

        [Fact]
        public void IntegrationTest_Given_UserForRegisterDtoWithEmptyUserNameEmailAndPassword_When_Register_Then_Return_OkStatus()
        {
            //Arrange           
            var expected = new UserForRegisterDto()
            {
                Username = "",
                Email = "",
                Password = ""
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/register", content).Result;

            User latestUser;
            using (var scope = _factory.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                latestUser = unitOfWork.User.GetLatestAsync().Result;
            } 
           

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            latestUser.UserName.Should().NotBe(expected.Username);

        }

        [Theory]
        [InlineData("Holly","password")]
        [InlineData("Zelma","password")]
        [InlineData("Admin","password")]
        public void IntegrationTest_Given_UserForLoginDto_When_Login_Then_Return_TokenAndUserWithOkStatus(string userName, string password)
        {
            //Arrange

            var expected = new UserForLoginDto()
            {
                Username = userName,
                Password = password
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/login", content).Result; 
           
            var responseContnent = httpRespone.Content.ReadAsStringAsync().Result;

            var tokenPatternForDeserializing = new {token = string.Empty};

            var token = JsonConvert.DeserializeAnonymousType(responseContnent, tokenPatternForDeserializing);

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.OK);
            token.token.As<string>().Should().NotBeEmpty();

        }

        [Theory]
        [InlineData("Holly","badpassword")]
        [InlineData("Zelma","badpassword")]
        [InlineData("Admin","badpassword")]
        public void IntegrationTest_Given_UserForLoginDtoWithBadPassword_When_Login_Then_Return_Unauthorized(string userName, string password)
        {
            //Arrange

            var expected = new UserForLoginDto()
            {
                Username = userName,
                Password = password
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/login", content).Result;

            var responseContent = httpRespone.Content.ReadAsStringAsync().Result;

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            responseContent.Should().NotContain("token");

        }

        [Fact]
        public void IntegrationTest_Given_UserForLoginDtoWithUserThatNotExist_When_Login_Then_Return_Unauthorized()
        {
            //Arrange

            var expected = new UserForLoginDto()
            {
                Username = "badusername",
                Password = "password"
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var httpRespone = _client.PostAsync("/api/auth/login", content).Result;

            var responseContnent = httpRespone.Content.ReadAsStringAsync().Result;

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            responseContnent.Should().NotContain("token");

        }
    }
}