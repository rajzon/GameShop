using GameShop.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameShop.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}