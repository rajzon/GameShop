using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
             builder.Property(p => p.Url)
                .IsRequired()
                .HasMaxLength(130);

            builder.Property(p => p.isMain)
                .IsRequired();
        }
    }
}