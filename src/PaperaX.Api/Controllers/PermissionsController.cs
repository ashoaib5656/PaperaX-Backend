using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PaperaX.Application.Features.Permissions.Queries.GetAllPermissions;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var result = await _mediator.Send(new GetAllPermissionsQuery());
            return Ok(result);
        }
    }
}
