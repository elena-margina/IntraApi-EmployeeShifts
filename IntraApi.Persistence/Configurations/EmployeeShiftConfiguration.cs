using IntraApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IntraApi.Persistence.Configurations
{
    public class EmployeeShiftConfiguration : IEntityTypeConfiguration<EmployeeShift>
    {
        public void Configure(EntityTypeBuilder<EmployeeShift> builder)
        {
            builder.HasKey(es => es.ID);

            builder.Property(es => es.EmployeeRoleID)
                .IsRequired();

            builder.Property(es => es.ShiftDate)
                .HasColumnType("date");

            builder.Property(es => es.StartTime)
                .HasColumnType("time");

            builder.Property(es => es.EndTime)
                .HasColumnType("time");

            builder.Property(es => es.Description)
                .HasMaxLength(int.MaxValue); 

            builder.Property(es => es.UserID)
                .IsRequired();

            builder.Property(es => es.DModify)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(es => es.Version)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(es => es.EmployeeRole)
                .WithMany(er => er.EmployeeShifts)
                .HasForeignKey(es => es.EmployeeRoleID)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
