using AutoMapper;
using IntraApi.Application.Contracts.Persistence;
using IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Features.EmployeeRoless.Queries.GetEmployeeRolesList
{
    public class GetEmployeesRolesListQueryHandler : IRequestHandler<GetEmployeesRolesListQuery, List<EmployeesRolesListVm>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetEmployeesRolesListQueryHandler(
            IMapper mapper,
            IEmployeeRepository employeeRepository,
            IEmployeeRoleRepository employeeRoleRepository,
            IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _employeeRoleRepository = employeeRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<List<EmployeesRolesListVm>> Handle(GetEmployeesRolesListQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.ListAllAsync();
            var employeeIds = employees.Select(e => e.ID).ToList();

            var employeeRoles = await _employeeRoleRepository.ListAllAsync(er => employeeIds.Contains(er.EmployeeID));
            var roleIds = employeeRoles.Select(er => er.RoleID).Distinct().ToList();
            var roles = await _roleRepository.ListAllAsync(r => roleIds.Contains(r.ID));

            var result = employees.Select(employee =>
            {
                var employeeRoleDtos = employeeRoles
                    .Where(er => er.EmployeeID == employee.ID)
                    .Select(er =>
                    {
                        var role = roles.FirstOrDefault(r => r.ID == er.RoleID);
                        if (role == null) return null;

                        var roleDto = _mapper.Map<RoleDto>(role);
                        roleDto.EmployeeRoleId = er.ID;
                        roleDto.RoleId = er.Role.ID;
                        roleDto.IsPrimary = er.Role.IsPrimary;
                        return roleDto;
                    })
                    .Where(dto => dto != null)
                    .ToList();

                var employeeVm = _mapper.Map<EmployeesRolesListVm>(employee);
                employeeVm.Roles = employeeRoleDtos!;
                return employeeVm;
            }).ToList();

            return result;
        }
    }

}
