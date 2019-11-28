using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.Exceptions
{
    public class ForbiddenOperationException : DotNetCoreArchitectureApplicationException
    {
        public ForbiddenOperationException()
        {
        }

        public ForbiddenOperationException(string message) : base(message)
        {
        }

        public ForbiddenOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
