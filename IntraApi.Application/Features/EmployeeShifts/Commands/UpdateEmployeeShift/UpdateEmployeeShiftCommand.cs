using IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Features.EmployeeShifts.Commands.UpdateEmployeeShift
{
    public class UpdateEmployeeShiftCommand : IRequest<UpdateEmployeeShiftCommandResponse>
    {
        public int EmployeeShiftId { get; set; }
        public int EmployeeRoleId { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
