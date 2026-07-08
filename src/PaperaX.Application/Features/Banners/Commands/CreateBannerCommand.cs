using MediatR;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.Banners.DTOs;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Banners.Commands
{
    public class CreateBannerCommand : IRequest<BannerDto>
    {
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string BannerType { get; set; } = "Static";
        public string TargetAudience { get; set; } = "All";
        public string Placement { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string? CtaText { get; set; }
        public string? CtaLink { get; set; }
        public string? ButtonStyle { get; set; }
        public string? BackgroundColor { get; set; }
        public string? TextColor { get; set; }
        public int Priority { get; set; }
        public int? PromotionId { get; set; }

        public string? DesktopImageUrl { get; set; }
        public string? TabletImageUrl { get; set; }
        public string? MobileImageUrl { get; set; }
        
        public decimal? MinCartValue { get; set; }
        public string? DeviceTarget { get; set; }
        public string? CountryTarget { get; set; }
        public int? MaxViewsPerUser { get; set; }
        
        public string? VariantGroup { get; set; }
    }

    public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, BannerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateBannerCommandHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<BannerDto> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {
            var endDate = request.EndDate;
            if (endDate.TimeOfDay == TimeSpan.Zero)
            {
                endDate = endDate.AddDays(1).AddTicks(-1); // End of the day
            }

            var banner = new Banner
            {
                Title = request.Title,
                ImageUrl = request.ImageUrl,
                BannerType = request.BannerType,
                TargetAudience = request.TargetAudience,
                Placement = request.Placement,
                DisplayOrder = request.DisplayOrder,
                StartDate = request.StartDate.ToUniversalTime(),
                EndDate = endDate.ToUniversalTime(),
                IsActive = request.IsActive,
                Subtitle = request.Subtitle,
                Description = request.Description,
                CtaText = request.CtaText,
                CtaLink = request.CtaLink,
                ButtonStyle = request.ButtonStyle,
                BackgroundColor = request.BackgroundColor,
                TextColor = request.TextColor,
                Priority = request.Priority,
                PromotionId = request.PromotionId,
                VariantGroup = request.VariantGroup,
                Asset = new BannerAsset
                {
                    DesktopImageUrl = request.DesktopImageUrl,
                    TabletImageUrl = request.TabletImageUrl,
                    MobileImageUrl = request.MobileImageUrl
                },
                TargetingRule = new BannerTargetingRule
                {
                    MinCartValue = request.MinCartValue,
                    DeviceTarget = request.DeviceTarget,
                    CountryTarget = request.CountryTarget,
                    MaxViewsPerUser = request.MaxViewsPerUser
                }
            };

            _context.Banners.Add(banner);
            await _context.SaveChangesAsync(cancellationToken);

            // Invalidate cache
            await _cacheService.RemoveAsync($"Banners_{banner.Placement}_True");
            await _cacheService.RemoveAsync($"Banners_{banner.Placement}_False");

            var dto = new BannerDto
            {
                Id = banner.Id,
                Title = banner.Title,
                ImageUrl = banner.ImageUrl,
                BannerType = banner.BannerType,
                TargetAudience = banner.TargetAudience,
                Placement = banner.Placement,
                DisplayOrder = banner.DisplayOrder,
                StartDate = banner.StartDate,
                EndDate = banner.EndDate,
                IsActive = banner.IsActive,
                Status = banner.Status,
                Subtitle = banner.Subtitle,
                Description = banner.Description,
                CtaText = banner.CtaText,
                CtaLink = banner.CtaLink,
                ButtonStyle = banner.ButtonStyle,
                BackgroundColor = banner.BackgroundColor,
                TextColor = banner.TextColor,
                Priority = banner.Priority,
                PromotionId = banner.PromotionId,
                VariantGroup = banner.VariantGroup,
                DesktopImageUrl = banner.Asset?.DesktopImageUrl,
                TabletImageUrl = banner.Asset?.TabletImageUrl,
                MobileImageUrl = banner.Asset?.MobileImageUrl,
                MinCartValue = banner.TargetingRule?.MinCartValue,
                DeviceTarget = banner.TargetingRule?.DeviceTarget,
                CountryTarget = banner.TargetingRule?.CountryTarget,
                MaxViewsPerUser = banner.TargetingRule?.MaxViewsPerUser
            };

            return dto;
        }
    }
}
