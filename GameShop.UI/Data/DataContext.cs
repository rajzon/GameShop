using GameShop.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.UI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :
         base(options) {}

         public DbSet<Value> Values { get; set; }

    }
}