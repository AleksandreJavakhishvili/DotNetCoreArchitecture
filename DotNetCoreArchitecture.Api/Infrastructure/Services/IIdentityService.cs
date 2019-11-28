using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Api.Infrastructure.Services
{
    public interface IIdentityService
    {
        Guid GetUserIdentity();
    }
}
