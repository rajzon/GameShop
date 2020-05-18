using System.Threading.Tasks;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user , string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExist(string username);
         
    }
}