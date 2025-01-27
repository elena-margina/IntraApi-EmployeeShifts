using MediatR;

namespace IntraApi.Application.Features.EmployeeShifts.Commands.DeleteEmployeeShift
{
    public class DeleteEmployeeShiftCommand : IRequest<DeleteEmployeeShiftCommandResponse>
    {
        public int EmployeeShiftId { get; set; }

    }
}