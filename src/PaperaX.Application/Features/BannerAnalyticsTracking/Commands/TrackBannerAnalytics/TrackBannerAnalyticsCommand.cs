using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using PaperaX.Domain.Entities;

namespace PaperaX.Application.Features.BannerAnalyticsTracking.Commands.TrackBannerAnalytics
{
    public class TrackBannerAnalyticsCommand : IRequest<bool>
    {
        public int BannerId { get; set; }
        public bool IsClick { get; set; } // true for click, false for view
    }

    public class TrackBannerAnalyticsCommandHandler : IRequestHandler<TrackBannerAnalyticsCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public TrackBannerAnalyticsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(TrackBannerAnalyticsCommand request, CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date;

            var analytics = await _context.BannerAnalytics
                .FirstOrDefaultAsync(a => a.BannerId == request.BannerId && a.Date == today, cancellationToken);

            if (analytics == null)
            {
                analytics = new BannerAnalytics
                {
                    BannerId = request.BannerId,
                    Date = today,
                    Views = request.IsClick ? 0 : 1,
                    Clicks = request.IsClick ? 1 : 0
                };
                _context.BannerAnalytics.Add(analytics);
            }
            else
            {
                if (request.IsClick)
                {
                    analytics.Clicks++;
                }
                else
                {
                    analytics.Views++;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
