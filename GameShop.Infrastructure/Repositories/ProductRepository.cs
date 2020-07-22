using GameShop.Application.Interfaces;
using GameShop.Domain.Model;

namespace GameShop.Infrastructure.Repositories
{
    public class ProductRepository: BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
            
        }

        
    }
}