using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Model;

namespace GameShop.Infrastructure.Repositories
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(ApplicationDbContext ctx)
            :base(ctx)
        {
            
        }
        
    }
}