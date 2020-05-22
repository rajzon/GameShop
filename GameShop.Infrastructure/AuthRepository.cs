using System;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;




namespace GameShop.Infrastructure
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _ctx;

        public AuthRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;

        }
        public async Task<User> Login(string username, string password)
        {
             var user = await _ctx.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if(user == null) return null;

            
            
            return user;
            
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using ( var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
           {              
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for (int i = 0; i< computedHash.Length;i++)
               {
                   if (computedHash[i] != passwordHash[i]) return false;                             
               }
           }
           return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using ( var hmac = new System.Security.Cryptography.HMACSHA512()) 
           {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash , passwordSalt;
            CreatePasswordHash(password ,out passwordHash , out passwordSalt);
  

            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExist(string username)
        {
            if(await _ctx.Users.AnyAsync(x => x.UserName == username)) 
                return true;

            return false;

        }
    }
}