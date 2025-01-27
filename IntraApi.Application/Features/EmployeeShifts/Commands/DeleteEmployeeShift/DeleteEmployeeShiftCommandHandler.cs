using AutoMapper;
using IntraApi.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Features.EmployeeShifts.Commands.DeleteEmployeeShift
{
    public class DeleteEmployeeShiftCommandHandler : IRequestHandler<DeleteEmployeeShiftCommand, DeleteEmployeeShiftCommandResponse>
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository;
        private readonly IEmployeeShiftViewRepository _employeeShiftViewRepository;
        private readonly IMapper _mapper;

        public DeleteEmployeeShiftCommandHandler(IMapper mapper, IEmployeeShiftRepository employeeShiftRepository, IEmployeeShiftViewRepository employeeShiftViewRepository)
        {
            _mapper = mapper;
            _employeeShiftRepository = employeeShiftRepository;
            _employeeShiftViewRepository = employeeShiftViewRepository;
        }

        public async Task<DeleteEmployeeShiftCommandResponse> Handle(DeleteEmployeeShiftCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteEmployeeShiftCommandResponse();
            var validator = new DeleteEmployeeShiftCommandValidator(_employeeShiftViewRepository);

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
                var employeeShiftToDelete = await _employeeShiftRepository.GetByIdAsync(request.EmployeeShiftId);
                await _employeeShiftRepository.DeleteAsync(employeeShiftToDelete);
            }
            return response;
        }
    }
}