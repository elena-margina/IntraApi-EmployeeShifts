namespace IntraApi.App.Models
{
    public class EmployeeShiftListVm
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string? PrimaryRoleName { get; set; } = string.Empty;
        public Dictionary<string, List<EmployeeShift>> ShiftsByDay { get; set; } = new();
    }

}
