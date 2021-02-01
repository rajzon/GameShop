using GameShop.Application.Interfaces;
using GameShop.Domain.Model;

namespace GameShop.Infrastructure.Repositories
{
    public class DeliveryOptRepository : BaseRepository<DeliveryOpt> , IDeliveryOptRepository
    {
        public DeliveryOptRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
        }
    }
}