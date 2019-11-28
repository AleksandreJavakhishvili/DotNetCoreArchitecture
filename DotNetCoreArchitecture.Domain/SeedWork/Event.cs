using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreArchitecture.SeedWork
{
    public abstract class Event : INotification
    {
        public DateTime CreateDate { get; }
        public bool IsCommitted { get; private set; }
        public string EventType { get; private set; }

        protected Event()
        {
            CreateDate = DateTime.Now;
            IsCommitted = false;
            EventType = GetType().Name;
        }
    }
}
