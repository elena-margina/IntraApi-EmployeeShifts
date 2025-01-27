
namespace IntraApi.Domain.Entities
{
    public class EmployeeShiftView
    {
        public int? EmployeeRoleID { get; set; }
        public int? EmployeeID { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public int? RoleID { get; set; }
        public bool? IsPrimaryRole { get; set; }
        public string? RoleName { get; set; } = string.Empty;
        public bool? IsAvailable { get; set; }
        public int? EmpoyeeShiftID { get; set; }
        public DateOnly? ShiftDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public string? EmployeeShiftDescr { get; set; } = string.Empty;
    }
}
