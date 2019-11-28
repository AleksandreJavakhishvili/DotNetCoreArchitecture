using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.Exceptions
{
    public class ConflictingOperationException : DotNetCoreArchitectureApplicationException
    {
        public ConflictingOperationException()
        {
        }

        public ConflictingOperationException(string message) : base(message)
        {
        }

        public ConflictingOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
