using DotNetCoreArchitecture.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.CommandBehaviours
{
    public class DomainEventPublishBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IDomainEventPublishRequest
    {
        private readonly ILogger<DomainEventPublishBehaviour<TRequest, TResponse>> _logger;
        private readonly IDomainEventContext _dbContext;
        private readonly IMediator _mediator;

        public DomainEventPublishBehaviour(IDomainEventContext dbContext,
            IMediator mediator,
            ILogger<DomainEventPublishBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = await next();

            var events = _dbContext.GetDomainEvents().ToList();

            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }

            return response;
        }
    }
}
