using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Features.Menus.Queries.GetNavigationMenu;
using PaperaX.Domain.Enums;
using System.Threading.Tasks;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("navigation")]
        public async Task<IActionResult> GetNavigationMenu([FromQuery] MenuPlacement placement, [FromQuery] string? roleName = null)
        {
            var query = new GetNavigationMenuQuery { Placement = placement, RoleName = roleName };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMenus()
        {
            var query = new PaperaX.Application.Features.Menus.Queries.GetAllMenus.GetAllMenusQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] PaperaX.Application.Features.Menus.Commands.CreateMenu.CreateMenuCommand command)
        {
            // Note: In real app, we'd get PerformedByUserId from claims
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] PaperaX.Application.Features.Menus.Commands.UpdateMenu.UpdateMenuCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var command = new PaperaX.Application.Features.Menus.Commands.DeleteMenu.DeleteMenuCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
