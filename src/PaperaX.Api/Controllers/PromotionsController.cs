using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.Promotions.Commands.CreatePromotion;
using PaperaX.Application.Features.Promotions.Commands.DeletePromotion;
using PaperaX.Application.Features.Promotions.Commands.UpdatePromotion;
using PaperaX.Shared.DTOs.Promotions;
using PaperaX.Application.Features.Promotions.Queries.GetAllPromotions;
using PaperaX.Application.Features.Promotions.Queries.GetPromotionById;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromotionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPromotionsQuery());
            return Ok(ApiResponse<object>.Success(result, "Promotion retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPromotionByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound(ApiResponse<object>.Failure("Promotion not found"));
            }
            return Ok(ApiResponse<object>.Success(result, "Promotion retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(ApiResponse<int>.Success(id, "Promotion created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePromotionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse<bool>.Failure("ID in URL does not match ID in command payload."));
            }

            await _mediator.Send(command);
            return Ok(ApiResponse<bool>.Success(true, "Promotion processed successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePromotionCommand { Id = id });
            return Ok(ApiResponse<bool>.Success(true, "Promotion processed successfully"));
        }
    }
}
