using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos.AddressDtos;
using GameShop.Domain.Model;

namespace GameShop.Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<Address> , IAddressRepository
    {
        public AddressRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
        }
    }
}