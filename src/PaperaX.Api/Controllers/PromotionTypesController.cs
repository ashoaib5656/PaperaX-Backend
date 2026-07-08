using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.PromotionTypes.Commands.CreatePromotionType;
using PaperaX.Application.Features.PromotionTypes.Commands.DeletePromotionType;
using PaperaX.Application.Features.PromotionTypes.Commands.UpdatePromotionType;
using PaperaX.Shared.DTOs.PromotionTypes;
using PaperaX.Application.Features.PromotionTypes.Queries.GetAllPromotionTypes;
using PaperaX.Application.Features.PromotionTypes.Queries.GetPromotionTypeById;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromotionTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPromotionTypesQuery());
            return Ok(ApiResponse<object>.Success(result, "Promotion Type retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPromotionTypeByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound(ApiResponse<object>.Failure("Promotion Type not found"));
            }
            return Ok(ApiResponse<object>.Success(result, "Promotion Type retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionTypeCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(ApiResponse<int>.Success(id, "Promotion Type created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePromotionTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse<bool>.Failure("ID in URL does not match ID in command payload."));
            }

            await _mediator.Send(command);
            return Ok(ApiResponse<bool>.Success(true, "Promotion Type processed successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePromotionTypeCommand { Id = id });
            return Ok(ApiResponse<bool>.Success(true, "Promotion Type processed successfully"));
        }
    }
}
