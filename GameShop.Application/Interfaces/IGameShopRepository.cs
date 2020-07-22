using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Application.Helpers;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;

namespace GameShop.Application.Interfaces
{
    public interface IGameShopRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<Product> GetProductWithPhotos(int productId);
         Task<Product> GetProduct(int productId);
         Task<PagedList<ProductForSearchingDto>> GetProductsForSearchingAsync(ProductParams productParams);
         Task<PagedList<ProductForModerationDto>> GetProductsForModerationAsync(ProductParams productParams);
         Task<Product> CreateProduct(ProductForCreationDto productForCreationDto, Requirements requirements, Category selectedCategory);
         Task<Product> EditProduct(int id,ProductToEditDto productForCreationDto, Requirements requirements, Category selectedCategory, Product productFromDb);
    }
}