
namespace IntraApi.Application.Features.Employees.Queries.GetEmployeesList
{
    public class EmployeeListVm
    {
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
