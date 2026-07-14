using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.Products.Commands.CreateProduct;
using PaperaX.Application.Features.Products.Commands.UpdateProduct;
using PaperaX.Application.Features.Products.Commands.DeleteProduct;
using PaperaX.Application.Features.Products.Queries.GetProducts;
using PaperaX.Application.Features.Products.Queries.GetProductById;
using System.Threading.Tasks;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(ApiResponse<object>.Success(products, "Products retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null) return NotFound(ApiResponse<object>.Failure($"Product with ID {id} not found"));
            return Ok(ApiResponse<object>.Success(product, "Product retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return Ok(ApiResponse<int>.Success(productId, "Product created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest(ApiResponse<bool>.Failure("ID mismatch"));
            
            var result = await _mediator.Send(command);
            if (!result) return NotFound(ApiResponse<bool>.Failure($"Product with ID {id} not found"));
            
            return Ok(ApiResponse<bool>.Success(true, "Product updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (!result) return NotFound(ApiResponse<bool>.Failure($"Product with ID {id} not found"));
            
            return Ok(ApiResponse<bool>.Success(true, "Product deleted successfully"));
        }
    }
}
