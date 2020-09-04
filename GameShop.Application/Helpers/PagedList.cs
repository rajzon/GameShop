using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Application.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }


        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, 
            int pageNumber, int pageSize) 
        {
            if (pageNumber < 1 && pageSize < 1)
            {
                throw new ArgumentException("PageNumber and PageSize are less then one");
            } else if (pageNumber < 1)
            {
                throw new ArgumentException("PageNumber is less then one");
            } else if (pageSize < 1)
            {
                throw new ArgumentException("PageSize is less then one");
            }

            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            return new PagedList<T>(items, count, pageNumber , pageSize);
        }

    }
}