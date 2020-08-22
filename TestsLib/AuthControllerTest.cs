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
    //TO DO: 
    // 1.On Login Method I have to delete returning user , in order to secure user info
    // 2.Retrun CretedAtRoute on Register Method
    // 3.Move GenerateJwtToken Method to Another Class
    // 4.Test Expires time for token 
    // 5. GenerateJwtTokenTest should be in unit test library
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

            var responseContnent = httpRespone.Content.ReadAsStringAsync().Result;

            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            responseContnent.Should().NotContain("token");

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


        [Theory]
        [InlineData(1, "Holly","Customer")]
        [InlineData(6, "Logan","Customer")]
        [InlineData(10, "Morales", "Customer")]
        [InlineData(11, "Admin", "Admin")]
        public void IntegrationTest_Given_CustomerUser_When_GenerateJwtToken_Then_Return_StringWithToken(int userId, string userName, string role)
        {
            //Arrange

            var expected = new User()
            {
                Id = userId,
                UserName = userName
                
            };
            var jwtHandler = new JwtSecurityTokenHandler();

            //Act
            string result;
            TokenValidationParameters tokenValidationParameters;
            using (var scope = _factory.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                var sut = new AuthController(config, userManager, signInManager);

                var generateJwtToken = sut.GetType()
                        .GetMethod("GenerateJwtToken", BindingFlags.NonPublic | BindingFlags.Instance);

                var task = (Task<string>)generateJwtToken.Invoke(sut, new object[] { expected });

                result = task.Result;


                tokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(config.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }

        

            SecurityToken securityToken;
            //Assert
            result.Should().NotBeEmpty();
            jwtHandler.CanReadToken(result).Should().BeTrue();

            ClaimsPrincipal principal = jwtHandler.ValidateToken(result, tokenValidationParameters, out securityToken);
            principal.Identity.IsAuthenticated
                    .Should().BeTrue();  
            principal.Identity.Name.Should().Be(expected.UserName);
            principal.IsInRole(role)
                    .Should().BeTrue();

            string nameIdentifierClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            var userIdIdentifier = principal.Claims
                    .Where(c => c.Type == nameIdentifierClaimType).FirstOrDefault().Value;
            userIdIdentifier.Should().Be(userId.ToString());

            string nameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"; 
            var username = principal.Claims
                    .Where(c => c.Type == nameClaimType).FirstOrDefault().Value;
            username.Should().Be(userName);
                     
        }
    }
}