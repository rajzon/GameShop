using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(o => o.SurName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(o => o.Address)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(o => o.Street)
                .HasMaxLength(80)
                .IsRequired();
                
            builder.Property(o => o.PostCode)
                .HasMaxLength(6)
                .IsRequired();

            builder.Property(o => o.City)
                .HasMaxLength(40);

            builder.Property(o => o.OrderRef)
                .IsRequired();
        
            builder.Property(o => o.StripeRef)
                .HasMaxLength(35)
                .IsRequired();

           builder.Property(o => o.OrderPrice)
                .HasColumnType("decimal(9,2)")
                .IsRequired();


        }
    }
}