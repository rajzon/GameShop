using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(p => p.Pegi)
                .IsRequired();
        
        }
    }
}