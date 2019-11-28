using DotNetCoreArchitecture.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.DomainEventHandlers
{
    public class PostCreatedDomainEventHandler : INotificationHandler<PostCreatedDomainEvent>
    {
        public Task Handle(PostCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // handle notification
            
            return Task.CompletedTask;
        }
    }
}
