using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.TotalAmount).IsRequired().HasMaxLength(100);
            builder.Property(x => x.OrderDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x=>x.Status).IsRequired().HasMaxLength(100);



            builder.HasCheckConstraint("CK_TotalAmount", "TotalAmount >= 0");

            
        }
    }
}
