using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IntraApi.Domain.Common;

namespace IntraApi.Domain.Entities
{
    [Table("Roles", Schema = "staffing")]
    public class Role : IAuditableEntity
    {
        [Key]
        public int ID { get; set; }
        public bool IsPrimary { get; set; }
        public int SeatsAvailable { get; set; }
        public bool IsAvailable { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UserID { get; set; }
        public DateTime DModify { get; set; } = DateTime.Now;
        public int Version { get; set; } = 0;
        public User? User { get; set; } = default!;
        public ICollection<EmployeeRole>? EmployeeRoles { get; set; } = default!;
    }
}
