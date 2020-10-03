using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class OrderStockConfiguration : IEntityTypeConfiguration<OrderStock>
    {
        public void Configure(EntityTypeBuilder<OrderStock> builder)
        {
            builder.Property(x => x.Quantity)
                    .IsRequired();

            builder.Property(x => x.Price)
                    .HasColumnType("decimal(9,2)")
                    .IsRequired();
        }
    }
}