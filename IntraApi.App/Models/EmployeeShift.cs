namespace IntraApi.App.Models
{
    public class EmployeeShift
    {
        public int ShiftId { get; set; }
        public int EmployeeRoleId { get; set; }
        public int? EmployeeId { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateOnly? ShiftDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }

}
