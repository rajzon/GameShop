using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.Domain.Dtos.UserDtos;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
            
        }

        public async Task<IEnumerable<UserForListDto>> GetAllWithRolesAsync()
        {
            var usersToReturn = await _ctx.Users.OrderBy(x => x.UserName)
            .Select(user => new UserForListDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = (from userRole in user.UserRoles
                         join role in _ctx.Roles
                         on userRole.RoleId
                         equals role.Id
                         select role.Name).ToList()
            }).ToListAsync();

            return usersToReturn;
        }

    public async Task<UserForAccInfoDto> GetForAccInfoAsync(int id)
        {
            return await _ctx.Users.Where(u => u.Id == id).Select(u => new UserForAccInfoDto
            {
                Name = u.UserName,
                SurName = u.SurName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).FirstOrDefaultAsync();
        }
    }
}