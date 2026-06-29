using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Features.Brands.Commands.CreateBrand;
using PaperaX.Application.Features.Brands.Commands.UpdateBrand;
using PaperaX.Application.Features.Brands.Queries.GetBrands;
using PaperaX.Application.Features.Brands.Queries.GetBrandById;
using PaperaX.Application.Features.Brands.Commands.DeleteBrand;
using PaperaX.Application.Common.Models;
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
            return Ok(ApiResponse<int>.Success(brandId, "Brand created successfully"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _mediator.Send(new GetBrandsQuery());
            return Ok(ApiResponse<object>.Success(brands, "Brands retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _mediator.Send(new GetBrandByIdQuery(id));
            if (brand == null) return NotFound(ApiResponse<object>.Failure($"Brand with ID {id} not found"));
            return Ok(ApiResponse<object>.Success(brand, "Brand retrieved successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandCommand command)
        {
            if (id != command.Id) return BadRequest(ApiResponse<bool>.Failure("ID mismatch"));
            
            var result = await _mediator.Send(command);
            if (!result) return NotFound(ApiResponse<bool>.Failure($"Brand with ID {id} not found"));
            
            return Ok(ApiResponse<bool>.Success(true, "Brand updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (!result) return NotFound(ApiResponse<bool>.Failure($"Brand with ID {id} not found"));
            
            return Ok(ApiResponse<bool>.Success(true, "Brand deleted successfully"));
        }
    }
}
