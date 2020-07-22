using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure
{
    public abstract class BaseRepository<T> : IBaseRepository<T> 
        where T: class
    {
        protected readonly ApplicationDbContext _ctx;

        public BaseRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;

        }
        public async void Add(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
        }

        public async void Delete(int id)
        {
          var entity =  await _ctx.Set<T>().FindAsync(id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
           return  await _ctx.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _ctx.Set<T>().Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllOrderedByAsync<TKey>(Expression<Func<T,TKey>> expression)
        {
            return await _ctx.Set<T>().OrderBy(expression).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllOrderedByDescAsync<TKey>(Expression<Func<T,TKey>> expression)
        {
            return await _ctx.Set<T>().OrderByDescending(expression).ToListAsync();
        }

    }
}