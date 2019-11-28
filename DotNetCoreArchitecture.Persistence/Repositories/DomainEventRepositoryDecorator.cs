using DotNetCoreArchitecture.Application.Interfaces;
using DotNetCoreArchitecture.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Persistence.Repositories
{
    public class DomainEventRepositoryDecorator<T> : IRepository<T> where T : IAggregateRoot
    {
        protected readonly DotNetCoreArchitectureContext Context;

        protected readonly IRepository<T> _repository;

        public DomainEventRepositoryDecorator(IRepository<T> repository, DotNetCoreArchitectureContext context)
        {
            Context = context;
            _repository = repository;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> AddAsync(T aggregate)
        {
            await AddDomainEvents(aggregate);

            return await _repository.AddAsync(aggregate);
        }

        public async Task RemoveAsync(T aggregate)
        {
            await AddDomainEvents(aggregate);

            await _repository.RemoveAsync(aggregate);
        }

        public async Task<T> UpdateAsync(T aggregate)
        {
            await AddDomainEvents(aggregate);

            await _repository.UpdateAsync(aggregate);

            return aggregate;
        }

        protected async Task AddDomainEvents(IAggregateRoot aggregateRoot)
        {
            var events = aggregateRoot.DomainEvents ?? new List<Event>();

            await AddDomainEvents(events);
        }

        protected async Task AddDomainEvents(IEnumerable<Event> events)
        {
            await Context.DomainEvents.AddRangeAsync(events.Select(domainEvent =>
                new DomainEvent(Guid.NewGuid(), JsonSerializer.Serialize(domainEvent), domainEvent.EventType)));
        }
    }
}
