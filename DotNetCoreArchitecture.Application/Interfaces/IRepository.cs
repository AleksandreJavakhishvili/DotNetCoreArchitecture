using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);

        Task<T> AddAsync(T aggregate);

        Task RemoveAsync(T aggregate);

        Task<T> UpdateAsync(T aggregate);

    }
}
