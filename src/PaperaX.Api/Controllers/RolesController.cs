using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaperaX.Application.Features.Roles.Queries.GetAllRoles;
using PaperaX.Application.Features.Roles.Queries.GetRoleById;
using PaperaX.Application.Features.Roles.Commands.CreateRole;
using PaperaX.Application.Features.Roles.Commands.UpdateRole;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery { Id = id });
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateRole(CreateRoleCommand command)
        {
            command.PerformedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub") ?? "Unknown";
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(int id, UpdateRoleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            command.PerformedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub") ?? "Unknown";
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
