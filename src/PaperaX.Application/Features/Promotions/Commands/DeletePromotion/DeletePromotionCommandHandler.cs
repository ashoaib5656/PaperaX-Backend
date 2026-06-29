using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperaX.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PaperaX.Application.Features.Promotions.Commands.DeletePromotion
{
    public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeletePromotionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _context.Promotions.FindAsync(new object[] { request.Id }, cancellationToken);

            if (promotion == null)
            {
                throw new Exception($"Promotion with id {request.Id} not found.");
            }

            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}

