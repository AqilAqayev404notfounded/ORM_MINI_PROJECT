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
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(100);
            builder.Property(x =>x.Price).IsRequired().HasMaxLength(100).HasColumnType("decimal(7,2)");
            builder.Property(x => x.Stock).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.CreatedDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UpdatedDate).HasDefaultValue(DateTime.UtcNow);




            builder.HasCheckConstraint("CK_Price", "Price > 0");
            builder.HasCheckConstraint("CK_Stock", "Stock >= 0");

        }
    }
}
