using DotNetCoreArchitecture.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetCoreArchitecture.Application.Commands
{
    [DataContract]
    public class CreatePostCommand : IRequest, ITransactionalRequest, IDomainEventPublishRequest, IIdempotentRequest
    {
        [DataMember]
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid CommandId { get; set; }
    }
}
