using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Domain.Dtos;
using GameShop.Domain.Dtos.UserDtos;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<UserForListDto>> GetAllWithRolesAsync();
        Task<UserForAccInfoDto> GetForAccInfoAsync(int id);
        
         
    }
}