using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Features.Categories.Commands.CreateCategory;
using PaperaX.Application.Features.Categories.Commands.UpdateCategory;
using PaperaX.Application.Features.Categories.Queries.GetCategories;
using PaperaX.Application.Features.Categories.Queries.GetCategoryById;
using PaperaX.Application.Features.Categories.Commands.DeleteCategory;
using PaperaX.Application.Common.Models;
using System.Threading.Tasks;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            return Ok(ApiResponse<int>.Success(categoryId, "Category created successfully"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            return Ok(ApiResponse<object>.Success(categories, "Categories retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null) return NotFound(ApiResponse<object>.Failure($"Category with ID {id} not found"));
            return Ok(ApiResponse<object>.Success(category, "Category retrieved successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
        {
            if (id != command.Id) return BadRequest(ApiResponse<bool>.Failure("ID mismatch"));
            
            var result = await _mediator.Send(command);
            if (!result) return NotFound(ApiResponse<bool>.Failure($"Category with ID {id} not found"));
            
            return Ok(ApiResponse<bool>.Success(true, "Category updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            if (!result) return NotFound(ApiResponse<bool>.Failure($"Category with ID {id} not found"));
            
            return Ok(ApiResponse<bool>.Success(true, "Category deleted successfully"));
        }
    }
}
