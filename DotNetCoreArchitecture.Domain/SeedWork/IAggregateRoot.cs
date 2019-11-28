using System.Collections.Generic;

namespace DotNetCoreArchitecture.SeedWork
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<Event> DomainEvents { get; }
    }
}
