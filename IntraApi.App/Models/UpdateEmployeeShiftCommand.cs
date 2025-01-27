namespace IntraApi.App.Models
{
    public class UpdateEmployeeShiftCommand
    {
        public int EmployeeShiftId { get; set; }
        public int EmployeeRoleID { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
