
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaperaX.Infrastructure.BackgroundJobs
{
    public class BannerStatusJob
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public BannerStatusJob(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task ExecuteAsync()
        {
            // The job's purpose is to find if any banner recently crossed a threshold
            // and clear the cache for affected placements.

            var now = DateTime.UtcNow;
            var oneMinuteAgo = now.AddMinutes(-1);

            // Banners that became active in the last minute or expired in the last minute
            var transitioningBanners = await _context.Banners
                .Where(b => b.IsActive && 
                            ((b.StartDate <= now && b.StartDate > oneMinuteAgo) || 
                             (b.EndDate <= now && b.EndDate > oneMinuteAgo)))
                .Select(b => b.Placement)
                .Distinct()
                .ToListAsync();

            foreach (var placement in transitioningBanners)
            {
                var cacheKey = $"banners:placement:{placement.ToLower()}";
                await _cacheService.RemoveAsync(cacheKey);
            }
        }
    }
}
