using IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList;
using IntraApi.Application.Features.EmployeeRoless.Queries.GetEmployeeRolesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntraApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRoleController : Controller
    {
        private readonly IMediator _mediator;

        public EmployeeRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetEmployeeRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeRolesListVm>> GetAllEmployeeRoles(int employeeId)
        {
            var getStEnrCQuery = new GetEmployeeRolesListQuery() { ID = employeeId };
            var res = await _mediator.Send(getStEnrCQuery);
            return Ok(res);
        }

        [HttpGet("GetEmployeesRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EmployeeRolesListVm>>> GetAllEmployeesRoles()
        {
            var res = await _mediator.Send(new GetEmployeesRolesListQuery() { });
            return Ok(res);
        }
    }
}
