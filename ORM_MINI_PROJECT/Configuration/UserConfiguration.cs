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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.Fulname).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
            builder.HasIndex(a=>a.Email).IsUnique();
            builder.Property(a => a.Password).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Address).IsRequired().HasMaxLength(100);
            


        }
    }
}
