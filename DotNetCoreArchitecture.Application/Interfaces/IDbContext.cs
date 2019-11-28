using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.Interfaces
{
    public interface IDbContext
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void RollbackTransaction();
        Task RetryOnExceptionAsync(Func<Task> func);

    }
}
