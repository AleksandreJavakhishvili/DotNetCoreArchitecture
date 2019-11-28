using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Persistence.Exceptions
{
    public class RockPaperScissorsPersistenceException : Exception
    {
        public RockPaperScissorsPersistenceException()
        {
        }

        public RockPaperScissorsPersistenceException(string message) : base(message)
        {
        }

        public RockPaperScissorsPersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
