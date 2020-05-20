using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure
{
    public class GameShopRepository : IGameShopRepository
    {
        private readonly ApplicationDbContext _ctx;

        public GameShopRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Add<T>(T entity) where T : class
        {
            _ctx.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _ctx.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            //Later on I am gonna add Addreses Table so probably I may want to Include Address Table here
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
             //Later on I am gonna add Addreses Table so probably I may want to Include Address Table here
            var users = await _ctx.Users.ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
           return await _ctx.SaveChangesAsync() > 0;
        }
    }
}