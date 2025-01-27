using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Features.EmployeeShifts.Queries
{
    public class EmployeeShiftDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string? PrimaryRoleName { get; set; } = string.Empty;
        public Dictionary<string, List<ShiftDto>> ShiftsByDay { get; set; } = new();
    }
}
