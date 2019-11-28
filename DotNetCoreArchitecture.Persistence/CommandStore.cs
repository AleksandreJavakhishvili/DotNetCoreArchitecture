using DotNetCoreArchitecture.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Persistence
{
    public class CommandStore : ICommandStore
    {
        public Task<bool> Exists(Guid id)
        {
            return Task.FromResult(false);
        }

        public Task Save(Guid id)
        {
            return Task.CompletedTask;
        }
    }
}
