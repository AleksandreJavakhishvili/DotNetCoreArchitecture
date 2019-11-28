using DotNetCoreArchitecture.Domain.Aggregates.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCoreArchitecture.Persistence.EntityConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post", DotNetCoreArchitectureContext.DefaultSchema);

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Status);
            builder.Property(p => p.Name);

            builder.ForNpgsqlUseXminAsConcurrencyToken();

            builder.Ignore(b => b.DomainEvents);

            builder.Property<byte[]>("RowVersion")
                .IsRowVersion();

        }
    }
}
