using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBrandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Brands.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.LogoUrl = request.LogoUrl;
            entity.WebsiteUrl = request.WebsiteUrl;
            entity.ContactEmail = request.ContactEmail;
            entity.ContactPhone = request.ContactPhone;
            entity.IsActive = request.IsActive;
            entity.UpdatedAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
