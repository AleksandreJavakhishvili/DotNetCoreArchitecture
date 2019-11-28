using DotNetCoreArchitecture.Application.Interfaces;
using DotNetCoreArchitecture.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Persistence.Repositories
{
    public class SqlRepository<TEntity, TIdentity> : IRepository<TEntity>
        where TEntity : EntityBase<TEntity, TIdentity>
    {
        protected readonly DotNetCoreArchitectureContext Context;

        public SqlRepository(DotNetCoreArchitectureContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TEntity> AddAsync(TEntity aggregate)
        {
            var res = await Context.Set<TEntity>().AddAsync(aggregate);

            return res.Entity;
        }

        public async Task AddListAsync(IEnumerable<TEntity> aggregates)
        {
            await Context.Set<TEntity>().AddRangeAsync(aggregates);
        }

        public Task RemoveAsync(TEntity aggregate)
        {
            Context.Entry(aggregate).State = EntityState.Deleted;

            return Task.CompletedTask;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var res = await Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Status == EntityStatus.Active && x.Id.Equals(id));

            return res;
        }

        public Task<TEntity> UpdateAsync(TEntity aggregate)
        {
            Context.Set<TEntity>().Update(aggregate);

            return Task.FromResult(aggregate);
        }

    }
}
