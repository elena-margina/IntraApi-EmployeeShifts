using MediatR;

namespace IntraApi.Application.Features.Users.Queries
{
    public class GetUsersListQuery : IRequest<List<UserListVm>>
    {
    }
}
