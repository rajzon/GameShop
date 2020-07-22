using System.Threading.Tasks;

namespace GameShop.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISubCategoryRepository SubCategory { get; } 
        ILanguageRepository Language { get; } 
        IPhotoRepository Photo { get; } 
        IUserRepository User { get; } 
        IProductRepository Product { get; } 
        Task Save();
    }
}