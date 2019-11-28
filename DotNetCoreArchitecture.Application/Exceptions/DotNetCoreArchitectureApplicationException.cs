using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.Exceptions
{
    public class DotNetCoreArchitectureApplicationException : Exception
    {
        public DotNetCoreArchitectureApplicationException()
        {
        }

        public DotNetCoreArchitectureApplicationException(string message) : base(message)
        {
        }

        public DotNetCoreArchitectureApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
