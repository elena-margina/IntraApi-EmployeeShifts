using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IntraApi.Domain.Entities;

namespace IntraApi.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.UserID)
                .IsRequired();

            //builder.HasOne(e => e.User) // Assuming the Employee has a navigation property to the User entity
            //    .WithMany() // Modify based on your actual User-Employee relationship (if needed)
            //    .HasForeignKey(e => e.UserID)
            //    .OnDelete(DeleteBehavior.Restrict); // Configure delete behavior as per your requirements

            builder.Property(e => e.DModify)
                .IsRequired()
                .HasDefaultValueSql("getdate()");
            builder.Property(e => e.Version)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
