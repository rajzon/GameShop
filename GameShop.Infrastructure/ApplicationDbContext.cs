using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
         base(options) {}

         public DbSet<Value> Values { get; set; }
         public DbSet<User> Users { get; set; }   
                 
    }
}