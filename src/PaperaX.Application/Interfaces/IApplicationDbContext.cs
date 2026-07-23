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
        DbSet<PromotionType> PromotionTypes { get; }
        DbSet<Banner> Banners { get; }
        DbSet<BannerAnalytics> BannerAnalytics { get; }
        DbSet<BannerAsset> BannerAssets { get; }
        DbSet<BannerTargetingRule> BannerTargetingRules { get; }
        DbSet<BannerVersion> BannerVersions { get; }
        DbSet<Role> Roles { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<RolePermission> RolePermissions { get; }
        DbSet<RoleAudit> RoleAudits { get; }
        DbSet<Menu> Menus { get; }
        DbSet<MenuRole> MenuRoles { get; }
        DbSet<MenuAudit> MenuAudits { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction();
        Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy CreateExecutionStrategy();
    }
}
