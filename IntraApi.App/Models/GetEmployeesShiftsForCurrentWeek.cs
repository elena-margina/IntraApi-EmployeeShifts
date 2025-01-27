namespace IntraApi.App.Models
{
    public class GetEmployeesShiftsForCurrentWeek
    {
        public List<EmployeeShiftListVm> Employees { get; set; } = new();
    }

}
