using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Repositories
{
    public class PhotoRepository: BaseRepository<Photo> , IPhotoRepository
    {
        public PhotoRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
            
        }

        public async Task<IEnumerable<Photo>> GetPhotosForProduct(int productId)
        {
            var photo = await _ctx.Photos.Where(p => p.ProductId == productId).Select(p => new Photo {
                        Id = p.Id,
                        Url = p.Url,
                        DateAdded = p.DateAdded,
                        isMain = p.isMain,
                        ProductId = p.ProductId
                    }).ToListAsync();

            return photo;
        }
        
    }
}