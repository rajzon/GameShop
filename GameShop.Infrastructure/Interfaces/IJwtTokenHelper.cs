using System.Threading.Tasks;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace GameShop.Infrastructure.Interfaces
{
    public interface IJwtTokenHelper
    {
        Task<string> GenerateJwtToken(User user, UserManager<User> userManager, IConfiguration config);
    }
}