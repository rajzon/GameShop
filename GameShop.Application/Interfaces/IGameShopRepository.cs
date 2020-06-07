using System.Collections.Generic;
using System.Threading.Tasks;
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
         Task<IEnumerable<ProductForSearchingDto>> GetProductsForSearchingAsync();
         Task<IEnumerable<Category>> GetCategories();
         Task<IEnumerable<SubCategory>> GetSubCategories();
         Task<IEnumerable<Language>> GetLanguages();
         Task<Product> CreateProduct(ProductForCreationDto productForCreationDto, Requirements requirements, Category selectedCategory);
         Task<Product> EditProduct(int id,ProductToEditDto productForCreationDto, Requirements requirements, Category selectedCategory, Product productFromDb);
    }
}