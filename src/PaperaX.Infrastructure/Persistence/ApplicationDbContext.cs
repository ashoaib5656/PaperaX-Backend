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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasIndex(c => c.Code).IsUnique();
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
