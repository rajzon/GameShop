using System.Collections.Generic;
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
using Newtonsoft.Json;
using TestsLib.DataForTests;
using Xunit;

namespace TestsLib
{
    public class UsersControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;


        public UsersControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.WithWebHostBuilder(builder => 
            {
                    builder.ConfigureTestServices(services => 
                    {
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    });
            }).CreateClient();

            using (var scope = factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                Seed.SeedUsers(userManager, roleManager, config);
            }
        }
    }
}