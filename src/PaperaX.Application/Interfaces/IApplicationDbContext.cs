using Microsoft.EntityFrameworkCore;
using PaperaX.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Brand> Brands { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
