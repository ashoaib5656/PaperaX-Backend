using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Features.Brands.Commands.CreateBrand;
using PaperaX.Application.Features.Brands.Commands.UpdateBrand;
using PaperaX.Application.Features.Brands.Queries.GetBrands;
using PaperaX.Application.Features.Brands.Queries.GetBrandById;
using PaperaX.Application.Features.Brands.Commands.DeleteBrand;
using System.Threading.Tasks;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrandCommand command)
        {
            var brandId = await _mediator.Send(command);
            
            return Created($"/api/brands/{brandId}", new { Id = brandId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _mediator.Send(new GetBrandsQuery());
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _mediator.Send(new GetBrandByIdQuery(id));
            if (brand == null) return NotFound();
            return Ok(brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandCommand command)
        {
            if (id != command.Id) return BadRequest();
            
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (!result) return NotFound();
            
            return NoContent();
        }
    }
}
