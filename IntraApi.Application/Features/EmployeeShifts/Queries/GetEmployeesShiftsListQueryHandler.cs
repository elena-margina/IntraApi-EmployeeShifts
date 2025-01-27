using AutoMapper;
using IntraApi.Application.Contracts.Persistence;
using IntraApi.Domain.Entities;
using MediatR;

namespace IntraApi.Application.Features.EmployeeShifts.Queries
{
    public class GetEmployeesShiftsListQueryHandler : IRequestHandler<GetEmployeesShiftsListQuery, EmployeesShiftsListVm>
    {
        private readonly IEmployeeShiftViewRepository _employeeShiftViewRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetEmployeesShiftsListQueryHandler(
            IMapper mapper,
            IEmployeeShiftViewRepository employeeShiftViewRepository,
            IEmployeeRepository employeeRepository,
            IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _employeeShiftViewRepository = employeeShiftViewRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }

        public async Task<EmployeesShiftsListVm> Handle(GetEmployeesShiftsListQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _employeeShiftViewRepository.GetWeeklyShiftSchedule();

            var employeeDetails = await _employeeRepository.ListAllAsync();
            var employeeMap = employeeDetails.ToDictionary(e => e.ID, e => e.FullName);

            // Fetch all shifts, including role details, from the repository
            var allShifts = await _employeeShiftViewRepository.ListAllAsync();

            // Extract primary roles for employees
            var primaryRoles = allShifts
                .Where(shift => shift.IsPrimaryRole.GetValueOrDefault()) // Filter only primary roles
                .GroupBy(shift => shift.EmployeeID.GetValueOrDefault()) // Group by EmployeeId
                .ToDictionary(
                    group => group.Key,
                    group => group.First().RoleName // Get the first primary role name for each employee
                );

            var response = new EmployeesShiftsListVm
            {
                Employees = employeeDetails.Select(employee => new EmployeeShiftDto
                {
                    EmployeeId = employee.ID,
                    EmployeeName = employee.FullName,
                    PrimaryRoleName = primaryRoles.ContainsKey(employee.ID) ? primaryRoles[employee.ID] : "", 
                    ShiftsByDay = schedule.ContainsKey(employee.ID)
                        ? schedule[employee.ID].ToDictionary(
                            day => day.Key,
                            day => day.Value.Select(shift => new ShiftDto
                            {
                                ShiftId = shift.ShiftId,
                                EmployeeRoleId = shift.EmployeeRoleId,
                                EmployeeId = employee.ID,
                                RoleId = shift.RoleId,
                                Role = shift.Role,
                                ShiftDate = shift.ShiftDate,
                                StartTime = shift.StartTime,
                                EndTime = shift.EndTime
                            }).ToList()
                        )
                        : new Dictionary<string, List<ShiftDto>>() // If no schedule, return empty dictionary
                }).ToList()
            };

            return response;
        }
    }


}
