using IntraApi.Application.Features.EmployeeShifts.Commands.CreateEmployeeShift;
using IntraApi.Application.Features.EmployeeShifts.Commands.DeleteEmployeeShift;
using IntraApi.Application.Features.EmployeeShifts.Commands.UpdateEmployeeShift;
using IntraApi.Application.Features.EmployeeShifts.Queries;
using IntraApi.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntraApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeShiftController : Controller
    {
        private readonly IMediator _mediator;

        public EmployeeShiftController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetEmployeesShiftsForCurrentWeek")]
        public async Task<ActionResult<EmployeesShiftsListVm>> GetShifts()
        {
            var response = await _mediator.Send(new GetEmployeesShiftsListQuery());
            return Ok(response);
        }

        [HttpPost("AddEmployeeShift")]
        public async Task<ActionResult<AddEmployeeShiftCommandResponse>> Create([FromBody] AddEmployeeShiftCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("UpdateEmployeeShift")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UpdateEmployeeShiftCommandResponse>> Update([FromBody] UpdateEmployeeShiftCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteEmployeeShift")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DeleteEmployeeShiftCommandResponse>> Delete(DeleteEmployeeShiftCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
