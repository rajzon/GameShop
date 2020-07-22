using GameShop.Application.Interfaces;
using GameShop.Domain.Model;

namespace GameShop.Infrastructure.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
            
        }
        
    }
}