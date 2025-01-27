using IntraApi.Application.Features.Students.Queries.GetStudentsList;
using MediatR;

namespace IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList
{
    public class GetEmployeeRolesListQuery : IRequest<EmployeeRolesListVm>
    {
        public int ID { get; set; }
    }
}
