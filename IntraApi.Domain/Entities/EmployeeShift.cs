using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IntraApi.Domain.Common;

namespace IntraApi.Domain.Entities
{
    [Table("EmployeeShifts", Schema = "staffing")]
    public class EmployeeShift : IAuditableEntity
    {
        [Key]
        public int ID { get; set; }
        public int EmployeeRoleID { get; set; }
        public DateOnly? ShiftDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public int UserID { get; set; }
        public DateTime DModify { get; set; } = DateTime.Now;
        public int Version { get; set; } = 0;

        [ForeignKey(nameof(EmployeeRoleID))]
        public EmployeeRole EmployeeRole { get; set; } = default!;
    }
}
