using IntraApi.Application.Features.Roles.Queries.GetRolesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntraApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RoleListVm>>> GetAllRole()
        {
            var dtos = await _mediator.Send(new GetRolesListQuery());
            return Ok(dtos);
        }
    }
}
