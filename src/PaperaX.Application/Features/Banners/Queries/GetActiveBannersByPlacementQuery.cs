using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.Banners.DTOs;
using PaperaX.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Banners.Queries
{
    public class GetActiveBannersByPlacementQuery : IRequest<List<BannerDto>>
    {
        public string Placement { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
    }

    public class GetActiveBannersByPlacementQueryHandler : IRequestHandler<GetActiveBannersByPlacementQuery, List<BannerDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public GetActiveBannersByPlacementQueryHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<List<BannerDto>> Handle(GetActiveBannersByPlacementQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"banners:placement:{request.Placement.ToLower()}:auth:{request.IsAuthenticated}";

            var cachedData = await _cacheService.GetAsync<List<BannerDto>>(cacheKey);

            if (cachedData != null)
            {
                var cachedResult = ApplyABTesting(cachedData);
                return cachedResult;
            }

            var now = DateTime.UtcNow;

            var banners = await _context.Banners
                .Where(b => b.IsActive && b.StartDate <= now && b.EndDate >= now && b.Placement == request.Placement
                            && (b.TargetAudience == "All" || 
                               (request.IsAuthenticated && b.TargetAudience == "LoggedIn") || 
                               (!request.IsAuthenticated && b.TargetAudience == "Guests")))
                .OrderBy(b => b.DisplayOrder)
                .Select(b => new BannerDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    BannerType = b.BannerType,
                    TargetAudience = b.TargetAudience,
                    Placement = b.Placement,
                    DisplayOrder = b.DisplayOrder,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    IsActive = b.IsActive,
                    Status = b.Status,
                    Subtitle = b.Subtitle,
                    Description = b.Description,
                    CtaText = b.CtaText,
                    CtaLink = b.CtaLink,
                    ButtonStyle = b.ButtonStyle,
                    BackgroundColor = b.BackgroundColor,
                    TextColor = b.TextColor,
                    Priority = b.Priority,
                    PromotionId = b.PromotionId,
                    VariantGroup = b.VariantGroup,
                    DesktopImageUrl = b.Asset != null ? b.Asset.DesktopImageUrl : null,
                    TabletImageUrl = b.Asset != null ? b.Asset.TabletImageUrl : null,
                    MobileImageUrl = b.Asset != null ? b.Asset.MobileImageUrl : null,
                    MinCartValue = b.TargetingRule != null ? b.TargetingRule.MinCartValue : null,
                    DeviceTarget = b.TargetingRule != null ? b.TargetingRule.DeviceTarget : null,
                    CountryTarget = b.TargetingRule != null ? b.TargetingRule.CountryTarget : null,
                    MaxViewsPerUser = b.TargetingRule != null ? b.TargetingRule.MaxViewsPerUser : null
                })
                .ToListAsync(cancellationToken);

            // Cache for 5 minutes as a fallback, but we will invalidate aggressively on changes
            await _cacheService.SetAsync(cacheKey, banners, TimeSpan.FromMinutes(5));

            var result = ApplyABTesting(banners);

            return result;
        }

        private List<BannerDto> ApplyABTesting(List<BannerDto> banners)
        {
            // If multiple banners belong to the same VariantGroup, pick one randomly.
            var result = new List<BannerDto>();
            var grouped = banners.GroupBy(b => string.IsNullOrEmpty(b.VariantGroup) ? Guid.NewGuid().ToString() : b.VariantGroup);

            var random = new Random();
            foreach (var group in grouped)
            {
                var list = group.ToList();
                if (list.Count == 1)
                {
                    result.Add(list.First());
                }
                else
                {
                    // Randomly select one variant
                    int index = random.Next(list.Count);
                    result.Add(list[index]);
                }
            }

            return result.OrderBy(b => b.DisplayOrder).ToList();
        }
    }
}
