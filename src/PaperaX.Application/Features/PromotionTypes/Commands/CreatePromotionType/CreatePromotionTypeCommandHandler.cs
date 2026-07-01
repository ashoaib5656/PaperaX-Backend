using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.PromotionTypes.Commands.CreatePromotionType
{
    public class CreatePromotionTypeCommandHandler : IRequestHandler<CreatePromotionTypeCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreatePromotionTypeCommandHandler(IApplicationDbContext context) { _context = context; }

        public async Task<int> Handle(CreatePromotionTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new PromotionType
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                CreatedAt = System.DateTime.UtcNow
            };
            _context.PromotionTypes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
