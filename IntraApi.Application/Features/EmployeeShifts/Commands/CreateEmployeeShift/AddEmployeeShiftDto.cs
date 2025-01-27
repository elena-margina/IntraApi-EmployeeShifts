
namespace IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift
{
    public class AddEmployeeShiftDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public DateOnly DateOnly { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }

}
