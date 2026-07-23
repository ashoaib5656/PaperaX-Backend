using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaperaX.Domain.Entities;
using PaperaX.Application.Interfaces;
using System.Reflection;

namespace PaperaX.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<Promotion> Promotions => Set<Promotion>();
        public DbSet<PromotionType> PromotionTypes => Set<PromotionType>();
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerAnalytics> BannerAnalytics { get; set; }
        public DbSet<BannerAsset> BannerAssets { get; set; }
        public DbSet<BannerTargetingRule> BannerTargetingRules { get; set; }
        public DbSet<BannerVersion> BannerVersions { get; set; }
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<RoleAudit> RoleAudits => Set<RoleAudit>();
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<MenuRole> MenuRoles => Set<MenuRole>();
        public DbSet<MenuAudit> MenuAudits => Set<MenuAudit>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasIndex(c => c.Code).IsUnique();
            
            modelBuilder.Entity<Product>()
                .Property(p => p.Specifications)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new Dictionary<string, string>()
                );

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<MenuRole>()
                .HasKey(mr => new { mr.MenuId, mr.RoleId });

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Parent)
                .WithMany(m => m.Children)
                .HasForeignKey(m => m.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.TokenHash)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }
    }
}
