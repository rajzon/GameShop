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
        IRequirementsRepository Requirements { get; } 
        IStockRepository Stock { get; }
        IOrderRepository Order { get;} 
        IStockOnHoldRepository StockOnHold { get;}
        IAddressRepository Address { get; }
        IDeliveryOptRepository DeliveryOpt { get; } 
        Task<bool> SaveAsync();
        bool IsAnyEntityModified();
        bool IsAnyEntityAdded();
        bool IsAnyEntityDeleted();
    }
}