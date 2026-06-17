using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaperaX.Infrastructure.Persistence;
using System.Threading.Tasks;
using System.Linq;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Users
                .Where(u => u.Role == "Customer")
                .Select(u => new {
                    id = $"#PX-C0{Math.Abs(u.Id)}",
                    name = u.FullName,
                    company = u.Company,
                    email = u.Email,
                    phone = u.Phone,
                    type = u.Type,
                    orders = u.OrdersCount,
                    spent = $"₹{u.TotalSpent:N0}",
                    status = u.Status
                })
                .ToListAsync();

            return Ok(customers);
        }
    }
}
