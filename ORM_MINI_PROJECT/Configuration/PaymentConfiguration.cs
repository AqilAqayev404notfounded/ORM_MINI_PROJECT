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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.PaymentDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x=>x.Amount).IsRequired().HasMaxLength(100).HasColumnType("decimal(7,2)");

            builder.HasCheckConstraint("CK_Amount", "Amount > 0");



        }
    }
}
