using IntraApi.Application.Features.Employees.Queries.GetEmployeesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace IntraApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAllEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EmployeeListVm>>> GetAllEmployees()
        {
            var dtos = await _mediator.Send(new GetEmployeesListQuery());
            return Ok(dtos);
        }

    }
}
