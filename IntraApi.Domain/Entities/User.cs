using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IntraApi.Domain.Entities
{
    [Table("Users", Schema = "umUser")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[]? Password { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public DateTime D_Modify { get; set; } = DateTime.Now;
        public int Version { get; set; } = 0;

        public ICollection<Employee> Employees { get; set; } = default!;
        public ICollection<EmployeeWorkQuota> EmployeeWorkQuotas { get; set; } = default!;
        public ICollection<Role> Roles { get; set; } = default!;
    }
}
