using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameShop.Application.Interfaces
{
    public interface IBaseRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T,bool>> expression);
        Task<T> GetAsync(int id);
        Task<T> GetLatestAsync();
        Task<IEnumerable<T>> GetAllOrderedByAsync<TKey>(Expression<Func<T,TKey>> expression);
        Task<IEnumerable<T>> GetAllOrderedByDescAsync<TKey>(Expression<Func<T,TKey>> expression);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task DeleteRangeAsync(Expression<Func<T, bool>> expression);
         
    }
}