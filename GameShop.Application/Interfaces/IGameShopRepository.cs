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
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<Category> GetCategory(int categoryId);
         Task<Product> GetProductWithPhotos(int productId);
         Task<Product> GetProductForCard(int productId);
         Task<Product> GetProductForDelete(int productId);
         Task<Product> GetProductForEdit(int productId);
         Task<Photo> GetPhoto(int photoId);
         Task<ICollection<Photo>> GetPhotosForProduct(int productId);
         Task<Photo> GetMainPhotoForProduct(int productId);
         Task<PagedList<ProductForSearchingDto>> GetProductsForSearchingAsync(ProductParams productParams);
         Task<PagedList<ProductForModerationDto>> GetProductsForModerationAsync(ProductParams productParams);
         Task<IEnumerable<Category>> GetCategories();
         Task<IEnumerable<SubCategory>> GetSubCategories();
         Task<IEnumerable<Language>> GetLanguages();
         Task<Product> CreateProduct(ProductForCreationDto productForCreationDto, Requirements requirements, Category selectedCategory);
         Task<Product> EditProduct(int id,ProductToEditDto productForCreationDto, Requirements requirements, Category selectedCategory, Product productFromDb);
    }
}