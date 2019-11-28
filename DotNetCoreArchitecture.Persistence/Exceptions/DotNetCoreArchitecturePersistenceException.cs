using System;

namespace DotNetCoreArchitecture.Persistence.Exceptions
{
    public class DotNetCoreArchitecturePersistenceException : Exception
    {
        public DotNetCoreArchitecturePersistenceException()
        {
        }

        public DotNetCoreArchitecturePersistenceException(string message) : base(message)
        {
        }

        public DotNetCoreArchitecturePersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}