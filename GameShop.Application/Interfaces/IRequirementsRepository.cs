using System.Threading.Tasks;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IRequirementsRepository : IBaseRepository<Requirements>
    {
         Task<RequirementsForEditDto> GetRequirementsForProductAsync(int id);
         
    }
}