using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}