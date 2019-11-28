using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Persistence.EntityConfigurations
{
    public class DomainEventConfiguration : IEntityTypeConfiguration<DomainEvent>
    {
        public void Configure(EntityTypeBuilder<DomainEvent> builder)
        {
            builder.ToTable("DomainEvents", DotNetCoreArchitectureContext.DefaultSchema);

            builder.HasKey(p => p.Id);


            builder.Property(p => p.EventData).IsRequired();

            builder.Property(p => p.EventType).IsRequired();
        }
    }
}
