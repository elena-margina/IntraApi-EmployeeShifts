using IntraApi.Domain.Entities;
using AutoMapper;
using IntraApi.Application.Features.Employees.Queries.GetEmployeesList;
using IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList;
using IntraApi.Application.Features.Roles.Queries.GetRolesList;
using IntraApi.Application.Features.Users.Queries;
using IntraApi.Application.Features.EmployeeRoless.Queries.GetEmployeeRolesList;
using IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift;
using IntraApi.Application.Features.EmployeeShifts.Commands.UpdateEmployeeShift;

namespace IntraApi.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserListVm>().ReverseMap();

            CreateMap<Employee, EmployeeListVm>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeRolesListVm>().ReverseMap();
            CreateMap<Employee, EmployeesRolesListVm>();

            CreateMap<Role, RoleListVm>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<EmployeeRole, EmployeeRolesListVm>().ReverseMap();

            CreateMap<EmployeeRole, EmployeeRolesListVm>()
            .ForMember(dest => dest.Role, opt => opt.Ignore()).ReverseMap();

            CreateMap<EmployeeShift, AddEmployeeShiftDto>().ReverseMap();
            CreateMap<EmployeeShift, UpdateEmployeeShiftCommand>().ReverseMap();
        }
    }
}
