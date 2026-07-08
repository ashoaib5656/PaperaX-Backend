using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Banners.Commands
{
    public class DeleteBannerCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public DeleteBannerCommandHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<bool> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
        {
            var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (banner == null)
            {
                return false;
            }

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync(cancellationToken);

            // Invalidate cache
            await _cacheService.RemoveAsync($"Banners_{banner.Placement}_True");
            await _cacheService.RemoveAsync($"Banners_{banner.Placement}_False");

            return true;
        }
    }
}
