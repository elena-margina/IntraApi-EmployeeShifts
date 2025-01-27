using FluentValidation;
using IntraApi.Application.Contracts.Persistence;
using IntraApi.Application.Features.EmployeeShifts.Commands.UpdateEmployeeShift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Features.EmployeeShifts.Commands.DeleteEmployeeShift
{
    internal class DeleteEmployeeShiftCommandValidator : AbstractValidator<DeleteEmployeeShiftCommand>
    {
        private readonly IEmployeeShiftViewRepository _employeeShiftViewRepository;

        public DeleteEmployeeShiftCommandValidator(IEmployeeShiftViewRepository employeeShiftViewRepository)
        {
            _employeeShiftViewRepository = employeeShiftViewRepository;

            RuleFor(p => p.EmployeeShiftId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(e => e)
                .MustAsync(EmployeeShiftExists)
                .WithMessage("This shift doesn't exist.");

            RuleFor(e => e)
                .MustAsync(CanItBeDeleted)
                .WithMessage("The shift cannot be deleted because the shift date has passed.");

        }

        private async Task<bool> EmployeeShiftExists(DeleteEmployeeShiftCommand e, CancellationToken token)
        {
            return await _employeeShiftViewRepository.EmployeeShiftExists(e.EmployeeShiftId);
        }

        private async Task<bool> CanItBeDeleted(DeleteEmployeeShiftCommand e, CancellationToken token)
        {
            return await _employeeShiftViewRepository.CanItBeDeleted(e.EmployeeShiftId);
        }
    }
}
