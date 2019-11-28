using System;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.Interfaces
{
    public interface ICommandStore
    {
        Task<bool> Exists(Guid id);

        Task Save(Guid id);
    }
}
