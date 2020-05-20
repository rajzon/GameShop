using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using GameShop.Domain.Model;

namespace GameShop.Infrastructure
{
    public class Seed
    {
        
        public static void SeedUsers(ApplicationDbContext ctx)
        {
           if (!ctx.Users.Any()) 
           {
               var userData = System.IO.File.ReadAllText("../GameShop.Infrastructure/UserSeedData.json");
               var users = JsonConvert.DeserializeObject<List<User>>(userData);
               foreach (var user in users)
               {
                   byte[] passwordHash , passwordSalt;
                   CreatePasswordHash("password", out passwordHash , out passwordSalt);

                   user.PasswordHash = passwordHash;
                   user.PasswordSalt = passwordSalt;
                   user.UserName = user.UserName.ToLower();
                   ctx.Users.Add(user);
               }

               ctx.SaveChanges();
           }

        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using ( var hmac = new System.Security.Cryptography.HMACSHA512()) 
           {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }
    }
}