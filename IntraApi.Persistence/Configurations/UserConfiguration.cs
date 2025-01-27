using IntraApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Mail)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Mail)
                   .IsUnique();

            builder.Property(e => e.Password)
                   .IsRequired()
                   .HasMaxLength(255);
        }
    }
}
