using GameShop.Application.Interfaces;
using GameShop.Domain.Model;

namespace GameShop.Infrastructure
{
    public class SubCategoryRepository : BaseRepository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(ApplicationDbContext ctx)
            :base(ctx)
        {

        }
        
    }
}