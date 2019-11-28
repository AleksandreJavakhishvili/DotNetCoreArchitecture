using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Api.Infrastructure.Exceptions
{
    public class DotNetCoreArchitecturePresentationException : Exception
    {
        public DotNetCoreArchitecturePresentationException()
        {
        }

        public DotNetCoreArchitecturePresentationException(string message) : base(message)
        {
        }

        public DotNetCoreArchitecturePresentationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
