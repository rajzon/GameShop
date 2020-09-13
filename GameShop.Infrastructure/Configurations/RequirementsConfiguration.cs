using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class RequirementsConfiguration : IEntityTypeConfiguration<Requirements>
    {
        public void Configure(EntityTypeBuilder<Requirements> builder)
        {
            builder.Property(r => r.OS)
                .HasMaxLength(30);

            builder.Property(r => r.Processor)
                .HasMaxLength(100);
 
            builder.Property(r => r.HDD)
                .IsRequired();       

            builder.Property(r => r.GraphicsCard)
                .HasMaxLength(100);

            builder.Property(r => r.IsNetworkConnectionRequire)
                .IsRequired();

        }
    }
}