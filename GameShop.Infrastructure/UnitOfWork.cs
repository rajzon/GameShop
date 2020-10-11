using System.Linq;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;
        
        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        private IRequirementsRepository _requirements;
        public IRequirementsRepository Requirements
        {
            get { 
                if (_requirements == null)
                {
                    _requirements = new RequirementsRepository(_ctx);
                }
                return _requirements; 
            }
        }
        

        private IProductRepository _product;
        public IProductRepository Product
        {
            get { 
                if (_product == null)
                {
                    _product = new ProductRepository(_ctx);
                }
                return _product; 
            }
        }
        

        private IUserRepository _user;
        public IUserRepository User
        {
            get { 
                if (_user == null)
                {
                    _user = new UserRepository(_ctx);
                }
                return _user; 
            }
            
        }
        

        private IPhotoRepository _photo;
        public IPhotoRepository Photo
        {
            get { 
                if (_photo == null)
                {
                    _photo = new PhotoRepository(_ctx);
                } 
                return _photo; 

            }
        }
        

        private ILanguageRepository _language;
        public ILanguageRepository Language
        {
            get { 
                if (_language == null)
                {
                    _language = new LanguageRepository(_ctx);
                }

                return _language; 
            }
        }
        

        private ISubCategoryRepository _subCategory;
        public ISubCategoryRepository SubCategory
        {
            get {
                if (_subCategory == null)
                {   
                    _subCategory = new SubCategoryRepository(_ctx);
                } 
                 return _subCategory;            
            }
        }
        

        private ICategoryRepository _category;
        public ICategoryRepository Category
        {
            get {
                if (_category == null)
                {
                    _category = new CategoryRepository(_ctx);
                }
                return _category;
            }
        }

        private IStockRepository _stock;
        public IStockRepository Stock
        {
            get { 
                if (_stock == null)
                {
                    _stock = new StockRepository(_ctx);
                } 
                return _stock; 
            }
        }

        private IOrderRepository _order;
        public IOrderRepository Order
        {
            get { 
                    if(_order == null) 
                    {
                        _order = new OrderRepository(_ctx);
                    } 
                    return _order; 
                }
        }

        private IStockOnHoldRepository _stockOnHold;
        public IStockOnHoldRepository StockOnHold
        {
            get { 
                    if(_order == null) 
                    {
                        _stockOnHold = new StockOnHoldRepository(_ctx);
                    } 
                    return _stockOnHold; 
                }
        }
        
        public bool IsAnyEntityModified()
        {
            return _ctx.ChangeTracker.Entries().Any(x => x.State == EntityState.Modified);
   
        }

        public bool IsAnyEntityAdded()
        {
            return _ctx.ChangeTracker.Entries().Any(x => x.State == EntityState.Added);
   
        }
        

        public async Task<bool> SaveAsync()
        {
            return  await _ctx.SaveChangesAsync() > 0;
        }
    }
}