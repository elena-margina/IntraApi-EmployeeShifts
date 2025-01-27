using IntraApi.Application.Responses;

namespace IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift
{
    public class AddEmployeeShiftCommandResponse : BaseResponse
    {
        public AddEmployeeShiftCommandResponse() : base()
        {

        }

        public AddEmployeeShiftDto EmployeeShift { get; set; } = default!;
    }
}
