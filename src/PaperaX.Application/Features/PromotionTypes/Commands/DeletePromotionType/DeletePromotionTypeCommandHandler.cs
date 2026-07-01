using MediatR;
using PaperaX.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.PromotionTypes.Commands.DeletePromotionType
{
    public class DeletePromotionTypeCommandHandler : IRequestHandler<DeletePromotionTypeCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeletePromotionTypeCommandHandler(IApplicationDbContext context) { _context = context; }

        public async Task Handle(DeletePromotionTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PromotionTypes.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null) throw new System.Exception("NotFound");

            _context.PromotionTypes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
