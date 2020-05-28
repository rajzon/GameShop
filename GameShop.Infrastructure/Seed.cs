using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace GameShop.Infrastructure
{
    public class Seed
    {

        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("../GameShop.Infrastructure/SeedDataSource/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

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
                    userManager.AddToRoleAsync(user, "Customer");
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
            if (!ctx.Products.Any())
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
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}