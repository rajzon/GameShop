using System.Net;
using System.Net.Http;
using FluentAssertions;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using GameShop.UI;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace TestsLib
{
    public class UsersControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public UsersControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            }).CreateClient();

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
        public void Given_None_When_GetUsers_ThenReturn_OkStatusWithUsers()
        {
            //Arrange

            //Act
            var httpRespone = _client.GetAsync("/api/users").Result;


            //Assert
            httpRespone.StatusCode.Should().Be(HttpStatusCode.OK);
            httpRespone.Content.Should().NotBeNull();

        }
    }
}