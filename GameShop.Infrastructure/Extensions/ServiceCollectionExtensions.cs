using GameShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Infrastructure.ApplicationDbContext>(x => 
            {
                //x.UseLazyLoadingProxies();
                x.UseSqlite(configuration.GetConnectionString("DefaultConnection") , b => b.MigrationsAssembly("GameShop.Infrastructure"));
            }); 
        }
    }
}