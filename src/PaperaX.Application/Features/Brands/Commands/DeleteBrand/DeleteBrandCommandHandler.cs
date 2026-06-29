using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBrandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Brands.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.Brands.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
