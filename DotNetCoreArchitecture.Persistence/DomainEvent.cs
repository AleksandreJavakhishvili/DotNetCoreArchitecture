using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Persistence
{
    public class DomainEvent
    {
        public Guid Id { get; private set; }
        [Column(TypeName = "jsonb")]
        public string EventData { get; private set; }
        public string EventType { get; private set; }

        public DomainEvent(Guid id, string eventData, string eventType)
        {
            Id = id;
            EventData = eventData;
            EventType = eventType;
        }
    }
}
