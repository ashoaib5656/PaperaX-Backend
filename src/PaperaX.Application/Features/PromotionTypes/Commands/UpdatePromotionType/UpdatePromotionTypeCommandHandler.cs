using MediatR;
using PaperaX.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.PromotionTypes.Commands.UpdatePromotionType
{
    public class UpdatePromotionTypeCommandHandler : IRequestHandler<UpdatePromotionTypeCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePromotionTypeCommandHandler(IApplicationDbContext context) { _context = context; }

        public async Task Handle(UpdatePromotionTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PromotionTypes.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null) throw new System.Exception("NotFound");

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.IsActive = request.IsActive;
            entity.UpdatedAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
