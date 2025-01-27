using MediatR;

namespace IntraApi.Application.Features.Roles.Queries.GetRolesList
{
    public class GetRolesListQuery : IRequest<List<RoleListVm>>
    {
    }
}
