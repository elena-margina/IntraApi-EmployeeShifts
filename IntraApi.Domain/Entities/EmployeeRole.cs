using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IntraApi.Domain.Common;

namespace IntraApi.Domain.Entities
{
    [Table("EmployeeRoles", Schema = "staffing")]
    public class EmployeeRole : IAuditableEntity
    {
        [Key]
        public int ID { get; set; }
        public int EmployeeID { get; set; } 
        public int RoleID { get; set; }
        public DateTime RoleStartDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int UserID { get; set; }
        public DateTime DModify { get; set; } = DateTime.Now;
        public int Version { get; set; } = 0;
        [ForeignKey(nameof(EmployeeID))]
        public Employee Employee { get; set; } = default!;
        [ForeignKey(nameof(RoleID))]
        public Role Role { get; set; } = default!;
        public ICollection<EmployeeShift> EmployeeShifts { get; set; } = default!;
    }
}
