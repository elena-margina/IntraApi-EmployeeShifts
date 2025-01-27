
namespace IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList
{
    public class RoleDto
    {
        public int EmployeeRoleId { get; set; }
        public DateTime? RoleStartDate { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int SeatsAvailable { get; set; }
        public bool IsAvailable { get; set; } 
    }
}
