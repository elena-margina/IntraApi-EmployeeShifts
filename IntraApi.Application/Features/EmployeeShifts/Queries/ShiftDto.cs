
namespace IntraApi.Application.Features.EmployeeShifts.Queries
{
    public class ShiftDto
    {
        public int ShiftId { get; set; }
        public int EmployeeRoleId { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public bool? IsPrimaryRole { get; set; }
        public string? Role { get; set; } = string.Empty;
        public DateOnly ShiftDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
