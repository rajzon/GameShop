using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.Property(sc => sc.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(sc => sc.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}