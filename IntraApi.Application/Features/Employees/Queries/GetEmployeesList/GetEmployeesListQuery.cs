using MediatR;

namespace IntraApi.Application.Features.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQuery : IRequest<List<EmployeeListVm>>
    {
    }
}
