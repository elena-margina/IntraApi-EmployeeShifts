

using AutoMapper;
using IntraApi.Application.Contracts.Persistence;
using IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift;
using IntraApi.Domain.Entities;
using MediatR;

namespace IntraApi.Application.Features.EmployeeShifts.Commands.UpdateEmployeeShift
{
    public class UpdateEmployeeShiftCommandHandler : IRequestHandler<UpdateEmployeeShiftCommand, UpdateEmployeeShiftCommandResponse>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository;
        private readonly IEmployeeShiftViewRepository _employeeShiftViewRepository;
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeShiftCommandHandler(IMapper mapper, IEmployeeShiftRepository employeeShiftRepository, IEmployeeShiftViewRepository employeeShiftViewRepository, IEmployeeRoleRepository employeeRoleRepository)
        {
            _mapper = mapper;
            _employeeShiftRepository = employeeShiftRepository;
            _employeeShiftViewRepository = employeeShiftViewRepository;
            _employeeRoleRepository = employeeRoleRepository;
        }

        public async Task<UpdateEmployeeShiftCommandResponse> Handle(UpdateEmployeeShiftCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateEmployeeShiftCommandResponse();
            var shiftToUpdate = await _employeeShiftRepository.GetByIdAsync(request.EmployeeShiftId);

            var validator = new UpdateEmployeeShiftCommandValidator(_employeeShiftViewRepository);
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

                _mapper.Map(request, shiftToUpdate, typeof(UpdateEmployeeShiftCommand), typeof(EmployeeShift));

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
                    shiftToUpdate.EmployeeRoleID = employeeRoleID;
                }

                if (employeeRoleID != shiftToUpdate.EmployeeRoleID)
                    shiftToUpdate.EmployeeRoleID = employeeRoleID;

                await _employeeShiftRepository.UpdateAsync(shiftToUpdate);
            }
            return response;
        }
    }
}
