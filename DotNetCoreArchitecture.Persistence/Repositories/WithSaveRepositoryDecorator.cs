using DotNetCoreArchitecture.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Persistence.Repositories
{
    public class WithSaveRepositoryDecorator<T> : IRepository<T> where T : class
    {
        protected readonly DotNetCoreArchitectureContext Context;

        private readonly IRepository<T> _repository;

        public WithSaveRepositoryDecorator(IRepository<T> repository, DotNetCoreArchitectureContext context)
        {
            Context = context;
            _repository = repository;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> AddAsync(T aggregate)
        {
            var res = await _repository.AddAsync(aggregate);

            await Context.SaveChangesAsync();

            return res;
        }

    
        public async Task RemoveAsync(T aggregate)
        {
            await _repository.RemoveAsync(aggregate);

            await Context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T aggregate)
        {
            var res = await _repository.UpdateAsync(aggregate);

            await Context.SaveChangesAsync();

            return res;
        }

    }
}
