using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_MINI_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(x => x.Quantity).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PricePerItem).IsRequired().HasMaxLength(100).HasColumnType("decimal(7,2)");

            builder.HasCheckConstraint("CK_Quantity", "Quantity >= 0");
            builder.HasCheckConstraint("PricePerItem", "PricePerItem >= 0");

        }
    }
}
