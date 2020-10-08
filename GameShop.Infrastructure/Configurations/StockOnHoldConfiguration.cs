using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Infrastructure.Configurations
{
    public class StockOnHoldConfiguration : IEntityTypeConfiguration<StockOnHold>
    {
        public void Configure(EntityTypeBuilder<StockOnHold> builder)
        {
            builder.Property(s => s.StockQty)
                    .IsRequired();
                    
            builder.Property(s => s.SessionId)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(s => s.ExpireTime)
                    .IsRequired();
        }
    }
}