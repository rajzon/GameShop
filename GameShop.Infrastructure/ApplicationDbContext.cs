using GameShop.Domain.Model;
using GameShop.Extensions.Infrastructure;
using GameShop.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace GameShop.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
        { }
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Requirements> Requirements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CategorySubCategory> CategoriesSubCategories { get; set; }
        public DbSet<ProductSubCategory> ProductsSubCategories { get; set; }
        public DbSet<ProductLanguage> ProductsLanaguages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.SeedProducts();

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new RequirementsConfiguration());
            builder.ApplyConfiguration(new LanguageConfiguration());
            builder.ApplyConfiguration(new PhotoConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new SubCategoryConfiguration());


            builder.Entity<Product>()
                .HasOne(p => p.Requirements)
                .WithOne(r => r.Product)
                .HasForeignKey<Requirements>(r => r.ProductId);

            builder.Entity<CategorySubCategory>()
                .HasKey(k => new { k.CategoryId, k.SubCategoryId });

            builder.Entity<CategorySubCategory>()
                .HasOne(csc => csc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(csc => csc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CategorySubCategory>()
                .HasOne(csc => csc.SubCategory)
                .WithMany(sc => sc.Categories)
                .HasForeignKey(csc => csc.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.Entity<ProductSubCategory>()
                .HasKey(k => new { k.ProductId, k.SubCategoryId });

            builder.Entity<ProductSubCategory>()
                .HasOne(psc => psc.Product)
                .WithMany(p => p.SubCategories)
                .HasForeignKey(psc => psc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductSubCategory>()
                .HasOne(psc => psc.SubCategory)
                .WithMany(sc => sc.Products)
                .HasForeignKey(psc => psc.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<ProductLanguage>()
                .HasKey(k => new { k.ProductId, k.LanguageId });

            builder.Entity<ProductLanguage>()
                .HasOne(pl => pl.Product)
                .WithMany(p => p.Languages)
                .HasForeignKey(pl => pl.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductLanguage>()
                .HasOne(pl => pl.Language)
                .WithMany(l => l.Products)
                .HasForeignKey(pl => pl.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
        }

    }

}