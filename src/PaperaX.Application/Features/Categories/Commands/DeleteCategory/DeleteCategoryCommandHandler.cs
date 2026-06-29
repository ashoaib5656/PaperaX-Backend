using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);

            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
