using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IntraApi.Domain.Common;

namespace IntraApi.Domain.Entities
{

    [Table("Employees", Schema = "staffing")]
    public class Employee : IAuditableEntity
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int UserID { get; set; }
        public User? User { get; set; } = default!;
        public DateTime DModify { get; set; } = DateTime.Now;
        public int Version { get; set; } = 0;
        public ICollection<EmployeeWorkQuota>? EmployeeWorkQuotas { get; set; } = default!;
        public ICollection<EmployeeRole>? EmployeeRoles { get; set; } = default!;
    }
}
