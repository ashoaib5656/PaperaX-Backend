using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.Banners.Commands;
using PaperaX.Application.Features.Banners.DTOs;
using PaperaX.Application.Features.Banners.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BannersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanners()
        {
            var response = await _mediator.Send(new GetAllBannersQuery());
            return Ok(ApiResponse<object>.Success(response, "Banners retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannerById(int id)
        {
            var response = await _mediator.Send(new GetBannerByIdQuery { Id = id });
            if (response == null) return NotFound(ApiResponse<object>.Failure("Banner not found."));
            return Ok(ApiResponse<object>.Success(response, "Banner retrieved successfully."));
        }

        [HttpGet("placement/{placement}")]
        public async Task<IActionResult> GetActiveBannersByPlacement(string placement)
        {
            bool isAuthenticated = HttpContext.User?.Identity?.IsAuthenticated ?? false;
            var response = await _mediator.Send(new GetActiveBannersByPlacementQuery 
            { 
                Placement = placement,
                IsAuthenticated = isAuthenticated
            });
            return Ok(ApiResponse<object>.Success(response, "Banners retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner([FromBody] CreateBannerCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(ApiResponse<object>.Success(response, "Banner created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanner(int id, [FromBody] UpdateBannerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse<object>.Failure("ID mismatch."));
            }

            var response = await _mediator.Send(command);
            if (response == null) return NotFound(ApiResponse<object>.Failure("Banner not found."));
            return Ok(ApiResponse<object>.Success(response, "Banner updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var response = await _mediator.Send(new DeleteBannerCommand { Id = id });
            if (!response) return NotFound(ApiResponse<object>.Failure("Banner not found."));
            return Ok(ApiResponse<object>.Success(true, "Banner deleted successfully."));
        }

        [HttpPost("{id}/analytics/track")]
        public async Task<IActionResult> TrackAnalytics(int id, [FromQuery] bool isClick)
        {
            var response = await _mediator.Send(new PaperaX.Application.Features.BannerAnalyticsTracking.Commands.TrackBannerAnalytics.TrackBannerAnalyticsCommand { BannerId = id, IsClick = isClick });
            return Ok(ApiResponse<object>.Success(true, "Analytics tracked successfully."));
        }
    }
}
