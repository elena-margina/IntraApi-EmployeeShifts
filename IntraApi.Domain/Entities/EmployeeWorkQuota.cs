using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IntraApi.Domain.Common;
using System.Text.Json.Serialization;
using IntraApi.Domain.Enums;

namespace IntraApi.Domain.Entities
{

    [Table("EmployeeWorkQuota", Schema = "staffing")]
    public class EmployeeWorkQuota : IAuditableEntity
    {
        [Key]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public decimal MandatoryHours { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WorkPeriodType PeriodType { get; set; }
        [ForeignKey(nameof(EmployeeID))]
        public Employee Employee { get; set; } = default!;
        [ForeignKey(nameof(UserID))]
        public int UserID { get; set; }
        public DateTime DModify { get; set; } = DateTime.Now;
        public int Version { get; set; } = 0;
        public User? User { get; set; } = default!;
    }

}
