using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.Extensions.Configuration;
using GameShop.Application.Helpers;

namespace GameShop.Infrastructure
{
    public class Seed
    {

        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration appSettings)
        {
            if (!userManager.Users.Any())
            {
                var seedDataLocationOptions = appSettings.GetSection(SeedDataLocationOptions.SeedDataLocation).Get<SeedDataLocationOptions>();
                var currDirectory = Directory.GetCurrentDirectory();

                string userData;
                var users = new List<User>();
                if (!currDirectory.Contains("GameShop.UI"))
                {
                    var userSeedDataLocation = seedDataLocationOptions.UserSeedData;

                    var testProjectDirectory = Directory.GetParent(currDirectory).Parent.Parent.Parent.FullName;

                    var combined = Path.GetFullPath(Path.Combine(testProjectDirectory,userSeedDataLocation));

                    userData = System.IO.File.ReadAllText(combined);
                    users = JsonConvert.DeserializeObject<List<User>>(userData);   
                }
                else 
                {
                    userData = System.IO.File.ReadAllText("../GameShop.Infrastructure/SeedDataSource/UserSeedData.json");
                    users = JsonConvert.DeserializeObject<List<User>>(userData);
                }
                

                var roles = new List<Role>
                {
                    new Role{Name = "Customer"},
                    new Role{Name = "Moderator"},
                    new Role{Name = "Admin"}
                };


                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }


                foreach (var user in users)
                {
                    userManager.CreateAsync(user, "password").Wait();
                    userManager.AddToRoleAsync(user, "Customer").Wait();
                }


                //Creating admin
                var adminUser = new User
                {
                    UserName = "Admin",
                    Email = "admin@shop.eu"
                };

                var result = userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
                }
            }

        }

        public static void SeedProductsFKs(ApplicationDbContext ctx)
        {
            if (ctx.Products.Select(p => p.Category).Any(l => l == null))
            {


                var products = ctx.Products;
                int categoryId = 1;
                foreach (var product in products)
                {
                    if (categoryId > 3)
                    {
                        categoryId = 1;
                    }

                    ctx.Entry(product).Property("CategoryId").CurrentValue = categoryId++;
                }
                ctx.SaveChanges();
            }


        }
    }
}