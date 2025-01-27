using MediatR;

namespace IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift
{
    public class AddEmployeeShiftCommand : IRequest<AddEmployeeShiftCommandResponse>
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Description { get; set; } = string.Empty;
    }

}
