using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Repositories
{
    public class RequirementsRepository : BaseRepository<Requirements>, IRequirementsRepository
    {
        public RequirementsRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
            
        }

        public async Task<RequirementsForEditDto> GetRequirementsForProductAsync(int id) 
        {
            var result = await _ctx.Requirements.Where(r => r.ProductId == id)
                        .Select(requriements => new RequirementsForEditDto
                        {
                            OS = requriements.OS,
                            Processor = requriements.Processor,
                            RAM = requriements.RAM,
                            GraphicsCard = requriements.GraphicsCard,
                            HDD = requriements.HDD,
                            IsNetworkConnectionRequire = requriements.IsNetworkConnectionRequire
                        }).FirstOrDefaultAsync();

            return result;
        }

    }
}