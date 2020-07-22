using GameShop.Application.Interfaces;
using GameShop.Domain.Model;

namespace GameShop.Infrastructure
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
            
        }
    }
}