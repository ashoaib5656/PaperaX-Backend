using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Shared.DTOs.Coupons;
using PaperaX.Application.Features.Coupons.Commands.CreateCoupon;
using PaperaX.Application.Features.Coupons.Commands.UpdateCoupon;
using PaperaX.Application.Features.Coupons.Commands.DeleteCoupon;
using PaperaX.Application.Features.Coupons.Queries.GetCoupons;
using PaperaX.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CouponsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coupons = await _mediator.Send(new GetCouponsQuery());
            return Ok(ApiResponse<IEnumerable<CouponDto>>.Success(coupons, "Coupons retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coupon = await _mediator.Send(new GetCouponByIdQuery(id));
            if (coupon == null) 
                return NotFound(ApiResponse<CouponDto>.Failure($"Coupon with ID {id} not found"));
            return Ok(ApiResponse<CouponDto>.Success(coupon, "Coupon retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCouponDto dto)
        {
            var id = await _mediator.Send(new CreateCouponCommand { CouponDto = dto });
            return Ok(ApiResponse<int>.Success(id, "Coupon created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCouponDto dto)
        {
            if (id != dto.Id) 
                return BadRequest(ApiResponse<bool>.Failure("ID mismatch between URL and payload"));
            
            var result = await _mediator.Send(new UpdateCouponCommand { CouponDto = dto });
            if (!result) 
                return NotFound(ApiResponse<bool>.Failure($"Coupon with ID {id} not found"));
            
            return Ok(ApiResponse<bool>.Success(true, "Coupon updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCouponCommand(id));
            if (!result) 
                return NotFound(ApiResponse<bool>.Failure($"Coupon with ID {id} not found"));
            
            return Ok(ApiResponse<bool>.Success(true, "Coupon deleted successfully"));
        }
    }
}
