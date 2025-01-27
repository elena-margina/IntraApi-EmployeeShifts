using IntraApi.Application.Contracts.Persistence;
using IntraApi.Domain.Entities;
using AutoMapper;
using MediatR;

namespace IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList
{
    public class GetEmployeeRolesListQueryHandler : IRequestHandler<GetEmployeeRolesListQuery, EmployeeRolesListVm>
    {
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeRolesListQueryHandler(IMapper mapper, IEmployeeRoleRepository employeeRoleRepository, IRoleRepository roleRepository, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRoleRepository = employeeRoleRepository;
            _roleRepository = roleRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeRolesListVm> Handle(GetEmployeeRolesListQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.ID);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.ID} not found.");
            }

            var EmployeeRoles = await _employeeRoleRepository.ListAllAsync(e => e.EmployeeID == request.ID);

            if (!EmployeeRoles.Any())
            {
                return new EmployeeRolesListVm
                {
                    ID = employee.ID,
                    FullName = employee.FullName,
                    Role = new List<RoleDto>() 
                };
            }

            var roleIds = EmployeeRoles.Select(e => e.RoleID).Distinct().ToList();
            var roles = await _roleRepository.ListAllAsync(c => roleIds.Contains(c.ID));

            var roleDtos = new List<RoleDto>();
            foreach (var employeeRole in EmployeeRoles)
            {
                var role = roles.FirstOrDefault(c => c.ID == employeeRole.RoleID);
                if (role != null)
                {
                    var roleDto = _mapper.Map<RoleDto>(role);

                    roleDto.EmployeeRoleId = employeeRole.ID;
                    roleDtos.Add(roleDto);
                }
            }

            var employeeVm = _mapper.Map<EmployeeRolesListVm>(employee);
            employeeVm.Role = roleDtos; 

            return employeeVm;
        }
    }
}
