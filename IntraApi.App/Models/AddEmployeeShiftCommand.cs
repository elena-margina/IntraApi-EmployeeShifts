namespace IntraApi.App.Models
{
    public class AddEmployeeShiftCommand
    {
        public int? EmployeeId { get; set; }
        public int RoleId { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
