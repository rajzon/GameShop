using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.SurName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Street)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(x => x.PostCode)
                .HasMaxLength(12)
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(320)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(40);

            builder.Property(x => x.Country)
                .HasMaxLength(40);
        }
    }
}