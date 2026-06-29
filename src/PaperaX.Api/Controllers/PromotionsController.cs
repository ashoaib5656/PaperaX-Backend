using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllPromotionsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetPromotionByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreatePromotionCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePromotionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL does not match ID in command payload.");
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePromotionCommand { Id = id });
            return NoContent();
        }
    }
}
