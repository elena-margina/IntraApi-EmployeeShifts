using AutoMapper;
using IntraApi.Application.Contracts.Persistence;
using MediatR;

namespace IntraApi.Application.Features.Users.Queries
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<UserListVm>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserListVm>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var allUsers = (await _userRepository.ListAllAsync()).OrderBy(x => x.UserName);
            return _mapper.Map<List<UserListVm>>(allUsers);
        }
    }
}
