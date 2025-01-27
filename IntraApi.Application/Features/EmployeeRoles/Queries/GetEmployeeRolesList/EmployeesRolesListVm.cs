using IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Features.EmployeeRoless.Queries.GetEmployeeRolesList
{
    public class EmployeesRolesListVm
    {
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }

}
