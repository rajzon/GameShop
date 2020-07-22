using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Infrastructure.Repositories;

namespace GameShop.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;
        
        

        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
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
            get { return _user; }
            set { _user = value; }
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
        

        public async Task Save()
        {
           await _ctx.SaveChangesAsync();
        }
    }
}