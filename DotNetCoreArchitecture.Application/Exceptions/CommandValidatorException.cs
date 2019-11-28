using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.Exceptions
{
    public class CommandValidatorException : DotNetCoreArchitectureApplicationException
    {
        public CommandValidatorException()
        {
        }

        public CommandValidatorException(string message) : base(message)
        {
        }

        public CommandValidatorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
