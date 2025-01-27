using IntraApi.Domain.Enums;

namespace IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList
{
    public class EmployeeRolesListVm
    {
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<RoleDto> Role { get; set; } = default!;
    }
}
