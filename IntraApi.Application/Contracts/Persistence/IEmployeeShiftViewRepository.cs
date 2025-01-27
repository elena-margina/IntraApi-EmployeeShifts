using IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift;
using IntraApi.Application.Features.EmployeeShifts.Queries;
using IntraApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Contracts.Persistence
{
    public interface IEmployeeShiftViewRepository : IAsyncRepository<EmployeeShiftView>
    {
        Task<Dictionary<int, Dictionary<string, List<ShiftDto>>>> GetWeeklyShiftSchedule();
        bool BeDateOnly(DateOnly date);
        bool BeTimeOnly(TimeOnly time);
        Task<bool> EmployeeExists(int employeeID);
        Task<bool> RoleExists(int roleId);
        Task<bool> EmployeeRoleIsAddedCheck(int roleID, int employeeID);
        Task<bool> CheckOverlapAsync(int employeeId, DateOnly shiftDate, TimeOnly startTime, TimeOnly endTime);
        Task<bool> CheckOverlapAsync(int employeeShiftId, int employeeId, DateOnly shiftDate, TimeOnly startTime, TimeOnly endTime);
        Task<bool> EmployeeShiftExists(int employeeShiftId, int roleId, int employeeId);
        Task<bool> EmployeeShiftExists(int employeeShiftId);
        Task<bool> CanItBeDeleted(int employeeShiftId);
        Task<bool> CheckForWorkPrimaryRole(int roleID, int employeeID);
    }   

}
