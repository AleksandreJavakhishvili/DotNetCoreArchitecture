using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetCoreArchitecture.Domain.Exceptions
{
    public class DotNetCoreArchitectureDomainException : Exception
    {
        public DotNetCoreArchitectureDomainException()
        {
        }

        public DotNetCoreArchitectureDomainException(string message) : base(message)
        {
        }

        public DotNetCoreArchitectureDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
