using System;

namespace DotNetCoreArchitecture.Application.Interfaces
{
    public interface IIdempotentRequest
    {
        Guid CommandId { get; set; }
    }
}
