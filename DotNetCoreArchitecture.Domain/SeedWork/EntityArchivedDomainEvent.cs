using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.SeedWork
{
    public class EntityArchivedDomainEvent<TEntity, TIdentity> : Event
    {
        public TIdentity Id { get; private set; }

        public EntityArchivedDomainEvent(TIdentity id)
        {
            Id = id;
        }
    }
}
