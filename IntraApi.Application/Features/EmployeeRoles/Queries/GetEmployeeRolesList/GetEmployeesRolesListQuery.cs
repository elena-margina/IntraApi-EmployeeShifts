using MediatR;

namespace IntraApi.Application.Features.EmployeeRoless.Queries.GetEmployeeRolesList
{
    public class GetEmployeesRolesListQuery : IRequest<List<EmployeesRolesListVm>>
    {
    }

}
