using MediatR;
using Microsoft.Extensions.Logging;
using PaperaX.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Common.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<TRequest> _logger;

        public TransactionBehavior(IApplicationDbContext context, ILogger<TRequest> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);

            try
            {
                using var transaction = _context.BeginTransaction();
                
                response = await next();
                
                await _context.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                
                return response;
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "PaperaX Request: Transaction Failed for {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
