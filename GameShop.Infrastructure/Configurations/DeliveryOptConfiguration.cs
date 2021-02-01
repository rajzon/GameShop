using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class DeliveryOptConfiguration : IEntityTypeConfiguration<DeliveryOpt>
    {
        public void Configure(EntityTypeBuilder<DeliveryOpt> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired();
            
            builder.Property(x => x.Price)
                .HasColumnType("decimal(9,2)")
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(400);
        }
    }
}