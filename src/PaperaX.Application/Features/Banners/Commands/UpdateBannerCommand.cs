using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.Banners.DTOs;
using PaperaX.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using PaperaX.Domain.Entities;

namespace PaperaX.Application.Features.Banners.Commands
{
    public class UpdateBannerCommand : IRequest<BannerDto?>
    {
        public int Id { get; set; }
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

    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, BannerDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateBannerCommandHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<BannerDto?> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            var banner = await _context.Banners
                .Include(b => b.Asset)
                .Include(b => b.TargetingRule)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (banner == null)
            {
                return null;
            }

            banner.Title = request.Title;
            if (request.ImageUrl != null) banner.ImageUrl = request.ImageUrl; // Assuming don't clear if null on update unless explicitly wanted. Actually let's just assign.
            banner.ImageUrl = request.ImageUrl;
            banner.BannerType = request.BannerType;
            banner.TargetAudience = request.TargetAudience;
            banner.Placement = request.Placement;
            banner.DisplayOrder = request.DisplayOrder;
            
            var endDate = request.EndDate;
            if (endDate.TimeOfDay == TimeSpan.Zero)
            {
                endDate = endDate.AddDays(1).AddTicks(-1); // End of the day
            }
            
            banner.StartDate = request.StartDate.ToUniversalTime();
            banner.EndDate = endDate.ToUniversalTime();
            banner.IsActive = request.IsActive;
            banner.Subtitle = request.Subtitle;
            banner.Description = request.Description;
            banner.CtaText = request.CtaText;
            banner.CtaLink = request.CtaLink;
            banner.ButtonStyle = request.ButtonStyle;
            banner.BackgroundColor = request.BackgroundColor;
            banner.TextColor = request.TextColor;
            banner.Priority = request.Priority;
            banner.PromotionId = request.PromotionId;
            banner.VariantGroup = request.VariantGroup;
            banner.UpdatedAt = DateTime.UtcNow;

            if (banner.Asset == null) banner.Asset = new BannerAsset();
            banner.Asset.DesktopImageUrl = request.DesktopImageUrl;
            banner.Asset.TabletImageUrl = request.TabletImageUrl;
            banner.Asset.MobileImageUrl = request.MobileImageUrl;

            if (banner.TargetingRule == null) banner.TargetingRule = new BannerTargetingRule();
            banner.TargetingRule.MinCartValue = request.MinCartValue;
            banner.TargetingRule.DeviceTarget = request.DeviceTarget;
            banner.TargetingRule.CountryTarget = request.CountryTarget;
            banner.TargetingRule.MaxViewsPerUser = request.MaxViewsPerUser;

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
