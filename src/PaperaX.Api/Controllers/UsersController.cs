using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Features.Users.Commands.UpdateUser;
using PaperaX.Application.Features.Users.Commands.DeactivateUser;
using PaperaX.Application.Features.Users.Commands.DeleteUser;
using PaperaX.Application.Features.Users.Commands.ChangePassword;
using PaperaX.Application.Features.Users.Queries.GetCurrentUser;
using PaperaX.Shared.DTOs.Users;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using PaperaX.Application.Common.Models;

namespace PaperaX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Assuming you have JWT authentication, this should be uncommented in production
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Helper to get current user ID from claims. 
        // For development/mock purposes, we default to 1 if no claim is found.
        private int GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 1; // Default mock user ID
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            var query = new GetCurrentUserQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(ApiResponse<object>.Success(result, "User retrieved successfully"));
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateSettings([FromBody] UpdateAccountSettingsRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(ApiResponse<object>.Failure("Unauthorized"));

            var command = new UpdateUserCommand(int.Parse(userId), request);
            var result = await _mediator.Send(command);
            
            return Ok(ApiResponse<UserDto>.Success(result, "Account settings updated successfully."));
        }

        [HttpPut("me/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(ApiResponse<object>.Failure("Unauthorized"));

            var command = new ChangePasswordCommand(int.Parse(userId), request);
            await _mediator.Send(command);
            
            return Ok(ApiResponse<object>.Success(null, "Password updated successfully."));
        }

        [HttpDelete("me/deactivate")]
        public async Task<IActionResult> DeactivateAccount()
        {
            var userId = GetCurrentUserId();
            var command = new DeactivateUserCommand(userId);
            await _mediator.Send(command);
            return Ok(ApiResponse<bool>.Success(true, "Account deactivated successfully"));
        }

        [HttpDelete("me")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = GetCurrentUserId();
            var command = new DeleteUserCommand(userId);
            await _mediator.Send(command);
            return Ok(ApiResponse<bool>.Success(true, "Account deleted successfully"));
        }
    }
}
