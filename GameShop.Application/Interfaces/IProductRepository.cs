using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Application.Helpers;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IProductRepository: IBaseRepository<Product>
    {  
        Task<PagedList<ProductForModerationDto>> GetProductsForModerationAsync(ProductParams productParams);
        Task<PagedList<ProductForSearchingDto>> GetProductsForSearchingAsync(ProductParams productParams);
        Task<Product> CreateAsync(ProductForCreationDto productForCreationDto, Requirements requirements, Category selectedCategory);
        Task<Product> EditAsync(int id, ProductToEditDto productToEditDto, Requirements requirements, Category selectedCategory, Product productFromDb);
        Task<Product> GetWithPhotosOnly(int productId);
        Task<ProductToEditDto> GetProductToEditAsync(RequirementsForEditDto requirements, IEnumerable<Photo> photosFromRepo, int id);
        
    }
}