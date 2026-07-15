using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaperaX.Application.Common.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous] // Needs to accept logs from anonymous public users
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> _logger;

        public LogsController(ILogger<LogsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("client-error")]
        [RequestSizeLimit(2048)] // Limit payload size to 2KB to prevent abuse
        // [EnableRateLimiting("fixed")] // Assuming a policy is configured. We will rely on standard middleware if any, or just payload limit for now.
        public IActionResult LogClientError([FromBody] ClientErrorLogDto errorDto)
        {
            if (errorDto == null || string.IsNullOrEmpty(errorDto.Message))
            {
                return BadRequest("Invalid error payload.");
            }

            // Using structured logging to enrich Serilog event with properties including the SourceContext tag.
            // The "ClientError" tag allows filtering frontend crashes from server logs.
            using (_logger.BeginScope(new { SourceContext = "ClientError", BoundaryLevel = errorDto.BoundaryLevel }))
            {
                _logger.LogError(
                    "Client Exception: {Message} | Route: {Route} | Boundary: {BoundaryName} | ComponentStack: {ComponentStack} | UserId: {UserId} | Stack: {Stack}",
                    errorDto.Message,
                    errorDto.Route,
                    errorDto.BoundaryName,
                    errorDto.ComponentStack,
                    errorDto.UserId,
                    errorDto.Stack
                );
            }

            return Ok();
        }
    }
}
