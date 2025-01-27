using IntraApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IntraApi.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.ID);

            builder.Property(r => r.IsPrimary)
                .IsRequired();

            builder.Property(r => r.SeatsAvailable)
                .IsRequired();

            builder.Property(r => r.IsAvailable)
                .IsRequired();

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Description)
                .HasMaxLength(int.MaxValue);  

            builder.Property(r => r.UserID)
                .IsRequired();

            //builder.HasOne(r => r.User)
            //    .WithMany() 
            //    .HasForeignKey(r => r.UserID)
            //    .OnDelete(DeleteBehavior.Restrict); 

            builder.Property(r => r.DModify)
                .IsRequired()
                .HasDefaultValueSql("getdate()");
     
            builder.Property(r => r.Version)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}
