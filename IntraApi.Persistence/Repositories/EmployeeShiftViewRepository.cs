using IntraApi.Application.Contracts.Persistence;
using IntraApi.Application.Extensions;
using IntraApi.Application.Features.EmployeeShifts.Queries;
using IntraApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IntraApi.Persistence.Repositories
{
    internal class EmployeeShiftViewRepository : BaseRepository<EmployeeShiftView>, IEmployeeShiftViewRepository
    {
        public EmployeeShiftViewRepository(IntraApiDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Dictionary<int, Dictionary<string, List<ShiftDto>>>> GetWeeklyShiftSchedule()
        {
            var startOfWeek = DateOnly.FromDateTime(DateTime.Now.StartOfWeek(DayOfWeek.Monday)); // Convert DateTime to DateOnly
            var endOfWeek = startOfWeek.AddDays(7); // DateOnly allows AddDays

            var shifts = await _dbContext.EmployeeShiftsV
                .Where(shift => shift.ShiftDate >= startOfWeek && shift.ShiftDate < endOfWeek)  
                .OrderBy(shift => shift.EmployeeID)
                .ThenBy(shift => shift.ShiftDate)
                .ThenBy(shift => shift.StartTime)
                .ToListAsync();

            var daysOfWeek = Enum.GetNames(typeof(DayOfWeek)); // ["Sunday", "Monday", ..., "Saturday"]
            var schedule = new Dictionary<int, Dictionary<string, List<ShiftDto>>>();

            foreach (var shift in shifts)
            {
                var employeeId = shift.EmployeeID.GetValueOrDefault();
                var dayOfWeek = shift.ShiftDate?.DayOfWeek.ToString();

                if (!schedule.ContainsKey(employeeId))
                    schedule[employeeId] = daysOfWeek.ToDictionary(day => day, day => new List<ShiftDto>());

                schedule[employeeId][dayOfWeek].Add(new ShiftDto
                {
                    ShiftId = shift.EmpoyeeShiftID.GetValueOrDefault(),
                    EmployeeRoleId = shift.EmployeeRoleID.GetValueOrDefault(),
                    EmployeeId = shift.EmployeeID.GetValueOrDefault(),
                    Role = shift.RoleName,
                    RoleId = shift.RoleID.GetValueOrDefault(), 
                    IsPrimaryRole = shift.IsPrimaryRole,
                    ShiftDate = shift.ShiftDate.GetValueOrDefault(),
                    StartTime = shift.StartTime ?? TimeOnly.MinValue, 
                    EndTime = shift.EndTime ?? TimeOnly.MinValue
                });
            }

            return schedule;
        }

        public bool BeDateOnly(DateOnly date)
        {
            return date >= DateOnly.FromDateTime(DateTime.MinValue)
                            && date <= DateOnly.FromDateTime(DateTime.MaxValue);
        }

        public bool BeTimeOnly(TimeOnly time)
        {
            return time >= TimeOnly.MinValue && time <= TimeOnly.MaxValue;
        }
        
        public Task<bool> EmployeeExists(int employeeID)
        {
            var employeeExists = _dbContext.Employees.Any(e => e.ID == employeeID);

            return Task.FromResult(employeeExists);
        }
        
        public Task<bool> RoleExists(int roleId)
        {
            var roleExists = _dbContext.Roles.Any(e => e.ID == roleId);

            return Task.FromResult(roleExists);
        }

        public Task<bool> EmployeeRoleIsAddedCheck(int roleID, int employeeID)
        {
            var employeeRolesExists = _dbContext.EmployeeRoles.Any(e => e.RoleID == roleID & e.EmployeeID == employeeID);

            return Task.FromResult(employeeRolesExists);
        }
        
        public Task<bool> EmployeeShiftExists(int employeeShiftId, int roleId, int employeeId)
        {
            int employeeRoleId = _dbContext.EmployeeRoles.Where(e => e.RoleID == roleId & e.EmployeeID == employeeId).Select(x => x.ID).FirstOrDefault();
            var employeeShiftExists = _dbContext.EmployeeShifts.Any(e => e.ID == employeeShiftId && e.EmployeeRoleID == employeeRoleId);

            return Task.FromResult(employeeShiftExists);
        }

        public Task<bool> EmployeeShiftExists(int employeeShiftId)
        {
            var employeeShiftExists = _dbContext.EmployeeShifts.Any(e => e.ID == employeeShiftId);

            return Task.FromResult(employeeShiftExists);
        }

        public Task<bool> CheckOverlapAsync(int employeeId, DateOnly shiftDate, TimeOnly startTime, TimeOnly endTime)
        {
            var overlappingShifts = _dbContext.EmployeeShiftsV
                                                    .Where(e => e.EmployeeID == employeeId && e.ShiftDate == shiftDate)
                                                    .Where(e =>
                                                            // New shift starts before the existing shift ends and ends after the existing shift starts
                                                            (startTime <= e.EndTime && endTime >= e.StartTime)
                                                             //||  (startTime == e.EndTime || endTime == e.StartTime) // Consider adjacent shifts
                                                    )
                                                    .Any();

            return Task.FromResult(!overlappingShifts);
        }

        public Task<bool> CheckOverlapAsync(int employeeShiftId, int employeeId, DateOnly shiftDate, TimeOnly startTime, TimeOnly endTime)
        {
            var overlappingShifts = _dbContext.EmployeeShiftsV
                                                    .Where(e => e.EmployeeID == employeeId && e.ShiftDate == shiftDate)
                                                    .Where(e => (startTime <= e.EndTime && endTime >= e.StartTime) && e.EmpoyeeShiftID != employeeShiftId)
                                                    .Any();

            return Task.FromResult(!overlappingShifts);
        }

        public Task<bool> CanItBeDeleted(int employeeShiftId)
        {
            var canBeDeleted = _dbContext.EmployeeShifts.Where(e => e.ID == employeeShiftId).Select(x => x.ShiftDate).FirstOrDefault() >= DateOnly.FromDateTime(DateTime.UtcNow.Date);

            return Task.FromResult(canBeDeleted);
        }

        public async Task<bool> CheckForWorkPrimaryRole(int roleID, int employeeID)
        {
            var role = await _dbContext.Roles.FindAsync(roleID);

            if (role == null)
                return false;

            var employeeRoles = await _dbContext.EmployeeRoles
                .Where(e => e.EmployeeID == employeeID)
                .Join(
                    _dbContext.Roles, 
                    er => er.RoleID,  
                    r => r.ID,       
                    (er, r) => new     
                    {
                        er.EmployeeID,
                        er.RoleID,
                        r.IsPrimary,
                        RoleName = r.Name
                    })
                .ToListAsync();

            // Case 1: No roles assigned to the employee
            if (!employeeRoles.Any())
                return true;

            // Case 2: Employee already has the same role and only one Role
            if (employeeRoles.Count() == 1 && employeeRoles.Any(e => e.RoleID == roleID))
                return true;

            // Case 3: Employee already has a primary role assigned
            if (employeeRoles.Any(x => x.IsPrimary))
                return false;

            // Case 4: The role being checked is a primary role, and the employee has no primary roles
            if (!employeeRoles.Any(x => x.IsPrimary) && role.IsPrimary == true)
                return false;
             
            return true;
        }

    }
}
