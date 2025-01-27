using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Features.EmployeeShifts.Queries
{
    public class GetEmployeesShiftsListQuery : IRequest<EmployeesShiftsListVm> 
    {
    }
}
