using AutoMapper;
using IntraApi.Application.Contracts.Persistence;
using IntraApi.Domain.Entities;
using MediatR;


namespace IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift
{
    public class AddEmployeeShiftCommandHandler : IRequestHandler<AddEmployeeShiftCommand, AddEmployeeShiftCommandResponse>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository;
        private readonly IEmployeeShiftViewRepository _employeeShiftViewRepository;
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IMapper _mapper;

        public AddEmployeeShiftCommandHandler(IMapper mapper, IEmployeeShiftRepository employeeShiftRepository, IEmployeeShiftViewRepository employeeShiftViewRepository, IEmployeeRoleRepository employeeRoleRepository)
        {
            _mapper = mapper;
            _employeeShiftRepository = employeeShiftRepository;
            _employeeShiftViewRepository = employeeShiftViewRepository;
            _employeeRoleRepository = employeeRoleRepository;
        }

        public async Task<AddEmployeeShiftCommandResponse> Handle(AddEmployeeShiftCommand request, CancellationToken cancellationToken)
        {
            var response = new AddEmployeeShiftCommandResponse();

            var validator = new AddEmployeeShiftCommandValidator(_employeeShiftViewRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (response.Success)
            {
                var employeeRoles = await _employeeShiftViewRepository.ListAllAsync(e => e.EmployeeID == request.EmployeeId & e.RoleID == request.RoleId);

                int employeeRoleID = employeeRoles?.Select(x => x.EmployeeRoleID).FirstOrDefault() ?? 0;

                if (employeeRoleID == 0)
                {
                    var employeeRole = new EmployeeRole
                    {
                        EmployeeID = request.EmployeeId,
                        RoleID = request.RoleId,
                        RoleStartDate = DateTime.Now
                    };

                    employeeRole = await _employeeRoleRepository.AddAsync(employeeRole);

                    employeeRoleID = employeeRole.ID;
                }

                var shift = new EmployeeShift
                {
                    EmployeeRoleID = employeeRoleID,
                    ShiftDate = request.ShiftDate,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Description = request.Description
                };

                await _employeeShiftRepository.AddAsync(shift);

                response.EmployeeShift = _mapper.Map<AddEmployeeShiftDto>(shift);
                response.EmployeeShift.EmployeeId = request.EmployeeId;
                response.EmployeeShift.RoleId = request.RoleId;
            }
            return response;
        }
    }

}
