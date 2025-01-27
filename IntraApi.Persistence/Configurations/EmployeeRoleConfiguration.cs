using IntraApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntraApi.Persistence.Configurations
{
    public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
    {
        public void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            builder.HasKey(er => er.ID);

            builder.Property(er => er.EmployeeID)
                .IsRequired();

            builder.Property(er => er.RoleID)
                .IsRequired();

            builder.Property(er => er.RoleStartDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(er => er.Description)
                .HasMaxLength(int.MaxValue); 

            builder.Property(er => er.UserID)
                .IsRequired();

            builder.Property(er => er.DModify)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(er => er.Version)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(er => er.Employee)
                .WithMany(e => e.EmployeeRoles)
                .HasForeignKey(er => er.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(er => er.Role)
                .WithMany(r => r.EmployeeRoles)
                .HasForeignKey(er => er.RoleID)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
