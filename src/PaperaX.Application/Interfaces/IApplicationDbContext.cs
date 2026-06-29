using Microsoft.EntityFrameworkCore;
using PaperaX.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<Category> Categories { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Coupon> Coupons { get; }
        DbSet<Promotion> Promotions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction();
    }
}
