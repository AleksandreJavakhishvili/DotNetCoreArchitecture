using DotNetCoreArchitecture.Domain.Events;
using DotNetCoreArchitecture.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreArchitecture.Domain.Aggregates.PostAggregate
{
    public class Post : EntityBase<Post, Guid>, IAggregateRoot
    {
        public string Name { get; private set; }

        public Post(string name)
        {
            Id = Guid.NewGuid();
            Name = name;

            AddDomainEvent(new PostCreatedDomainEvent
            {
                Name = Name
            });
        }
    }
}
