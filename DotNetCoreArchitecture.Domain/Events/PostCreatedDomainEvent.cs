using DotNetCoreArchitecture.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreArchitecture.Domain.Events
{
    public class PostCreatedDomainEvent : Event
    {
        public string Name { get; set; }
    }
}
