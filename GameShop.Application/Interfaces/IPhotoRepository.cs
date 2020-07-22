using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IPhotoRepository: IBaseRepository<Photo>
    { 
        Task<IEnumerable<Photo>> GetPhotosForProduct(int productId);   
    }
}