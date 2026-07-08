using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.Banners.DTOs;
using PaperaX.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Banners.Queries
{
    public class GetAllBannersQuery : IRequest<List<BannerDto>>
    {
    }

    public class GetAllBannersQueryHandler : IRequestHandler<GetAllBannersQuery, List<BannerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllBannersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BannerDto>> Handle(GetAllBannersQuery request, CancellationToken cancellationToken)
        {
            var banners = await _context.Banners
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

            return banners;
        }
    }
}
