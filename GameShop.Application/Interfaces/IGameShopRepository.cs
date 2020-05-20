using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IGameShopRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}