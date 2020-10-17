using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using GameShop.Application.Helpers;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using GameShop.Infrastructure.Identity;
using GameShop.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace TestsLib
{
    //TO DO:
    // 4.Test Expires time for token 
    // Split 1 big method test into few small tests
    public class JwtTokenHelperTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public JwtTokenHelperTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData(1, "Holly","Customer")]
        [InlineData(6, "Logan","Customer")]
        [InlineData(10, "Morales", "Customer")]
        [InlineData(11, "Admin", "Admin")]
        public void Given_UserUserManagerIConfiguration_When_GenerateJwtToken_ThenReturn_Token(int userId, string userName, string role)
        {
            //Arrange
            var jwtHandler = new JwtSecurityTokenHandler();

            TokenValidationParameters tokenValidationParameters;
            string result;
            User user;        
            using (var scope = _factory.Services.CreateScope())
            {
                
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                Seed.SeedUsers(userManager, roleManager, config);


                var jwtOptions = Options.Create(config.GetSection("JWTSettings").Get<JWTSettings>());
                var sut = new JwtTokenHelper(jwtOptions);

                user = context.Users.Where(u => u.Id == userId).FirstOrDefault();

                tokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(config.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                //Act
                result = sut.GenerateJwtToken(user, userManager, config).Result;

            }

            SecurityToken securityToken;
            //Assert
            result.Should().NotBeEmpty();
            jwtHandler.CanReadToken(result).Should().BeTrue();

            ClaimsPrincipal principal = jwtHandler.ValidateToken(result, tokenValidationParameters, out securityToken);
            principal.Identity.IsAuthenticated
                    .Should().BeTrue();  
                    
            principal.Identity.Name.Should().Be(user.UserName);
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

        [Fact]
        public void Given_UserWhichIsNullUserManagerIConfiguration_When_GenerateJwtToken_ThenThrowArgumentNullException_BecauseNullUserWasPassed()
        {
            //Arrange
            int fakeUserId = 100;
            var jwtHandler = new JwtSecurityTokenHandler();

            TokenValidationParameters tokenValidationParameters;
            User nullUser;        
            using (var scope = _factory.Services.CreateScope())
            {
                
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                Seed.SeedUsers(userManager, roleManager, config);

                var jwtOptions = Options.Create(config.GetSection("JWTSettings").Get<JWTSettings>());
                var sut = new JwtTokenHelper(jwtOptions);

                nullUser = context.Users.Where(u => u.Id == fakeUserId).FirstOrDefault();

                tokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(config.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                //Act
                sut.Invoking(s => s.GenerateJwtToken(nullUser, userManager, config))
                            .Should().ThrowExactly<ArgumentNullException>()
                                    .Which.Message.Should().Contain("User cannot be null");
            }

    
            
        }
    }
}