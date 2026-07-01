using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<PromotionTypeDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllPromotionTypesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionTypeDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetPromotionTypeByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreatePromotionTypeCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePromotionTypeCommand command)
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
            await _mediator.Send(new DeletePromotionTypeCommand { Id = id });
            return NoContent();
        }
    }
}
