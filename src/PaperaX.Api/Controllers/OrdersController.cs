using Microsoft.AspNetCore.Mvc;
using PaperaX.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using PaperaX.Infrastructure.Persistence;
using System.Threading.Tasks;
using System.Linq;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders
                .Select(o => new {
                    id = o.StringId,
                    date = o.DateString,
                    customer = o.CustomerName,
                    total = o.TotalString,
                    payment = o.Payment,
                    status = o.Status,
                    itemsJson = o.ItemsJson,
                    itemSummary = o.ItemSummary,
                    estDelivery = o.EstDelivery,
                    destination = o.Destination
                })
                .ToListAsync();

            return Ok(orders);
        }
    }
}
