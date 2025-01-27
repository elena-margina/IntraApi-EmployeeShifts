using FluentValidation;
using IntraApi.Application.Contracts.Persistence;

namespace IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift
{
    public class AddEmployeeShiftCommandValidator : AbstractValidator<AddEmployeeShiftCommand>
    {
        private readonly IEmployeeShiftViewRepository _employeeShiftViewRepository;

        public AddEmployeeShiftCommandValidator(IEmployeeShiftViewRepository employeeShiftViewRepository)
        {
            _employeeShiftViewRepository = employeeShiftViewRepository;

            RuleFor(p => p.EmployeeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.RoleId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ShiftDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(BeDateOnly).WithMessage("{PropertyName} must be of type DateOnly."); ;

            RuleFor(p => p.StartTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(BeTimeOnly).WithMessage("{PropertyName} must be of type TimeOnly.");

            RuleFor(p => p.EndTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(BeTimeOnly).WithMessage("{PropertyName} must be of type TimeOnly.");

            RuleFor(p => p)
                .Must(p => p.StartTime < p.EndTime)
                .WithMessage("StartTime must be earlier than EndTime.");

            RuleFor(e => e)
                .MustAsync(EmployeeExists)
                .WithMessage("This employee does not exist.");

            RuleFor(e => e)
                .MustAsync(RoleExists)
                .WithMessage("This employee does not exist.");

            RuleFor(e => e)
                .MustAsync(CheckOverlapAsync)
                .WithMessage("The employee cannot have overlapping shifts in a single day.");

            RuleFor(e => e)
                .MustAsync(CheckForWorkPrimaryRole)
                .WithMessage("This role is primary and cannot be assigned to the selected employee because they already have another primary role or other non-primary roles assigned.");
        }

        private bool BeDateOnly(DateOnly date)
        {
            return _employeeShiftViewRepository.BeDateOnly(date);
        }
        private bool BeTimeOnly(TimeOnly time)
        {
            return _employeeShiftViewRepository.BeTimeOnly(time);
        }        
        private async Task<bool> EmployeeExists(AddEmployeeShiftCommand e, CancellationToken token)
        {
            return await _employeeShiftViewRepository.EmployeeExists(e.EmployeeId);
        }
        private async Task<bool> RoleExists(AddEmployeeShiftCommand e, CancellationToken token)
        {
            return await _employeeShiftViewRepository.RoleExists(e.RoleId);
        }        
        private async Task<bool> CheckForWorkPrimaryRole(AddEmployeeShiftCommand e, CancellationToken token)
        {
            return await _employeeShiftViewRepository.CheckForWorkPrimaryRole(e.RoleId, e.EmployeeId);
        }
        private async Task<bool> CheckOverlapAsync(AddEmployeeShiftCommand e, CancellationToken token)
        {
            return await _employeeShiftViewRepository.CheckOverlapAsync(e.EmployeeId, e.ShiftDate, e.StartTime, e.EndTime);
        }
    }
}

