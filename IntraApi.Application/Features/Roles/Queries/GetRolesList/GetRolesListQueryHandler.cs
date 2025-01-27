using MediatR;
using AutoMapper;
using IntraApi.Application.Contracts.Persistence;

namespace IntraApi.Application.Features.Roles.Queries.GetRolesList
{
    public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, List<RoleListVm>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRolesListQueryHandler(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleListVm>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var allRoles = (await _roleRepository.ListAllAsync()).OrderBy(x => x.Name);
            return _mapper.Map<List<RoleListVm>>(allRoles);
        }
    }
}
