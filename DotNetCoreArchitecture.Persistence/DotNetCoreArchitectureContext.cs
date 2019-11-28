using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetCoreArchitecture.Application.Interfaces;
using DotNetCoreArchitecture.Domain.Aggregates;
using DotNetCoreArchitecture.SeedWork;
using DotNetCoreArchitecture.Domain.Aggregates.PostAggregate;
using DotNetCoreArchitecture.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Design;

namespace DotNetCoreArchitecture.Persistence
{
    public class DotNetCoreArchitectureContext : DbContext, IDomainEventContext, IDbContext
    {
        public const string DefaultSchema = "DotNetCoreArchitecture";

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<DomainEvent> DomainEvents { get; set; }


        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        private IDbContextTransaction _currentTransaction;

        public DotNetCoreArchitectureContext(DbContextOptions<DotNetCoreArchitectureContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());

            modelBuilder.ApplyConfiguration(new DomainEventConfiguration());
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction ??= await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task RetryOnExceptionAsync(Func<Task> func)
        {
            await Database.CreateExecutionStrategy().ExecuteAsync(func);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public IEnumerable<Event> GetDomainEvents()
        {
            var domainEntities = this.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents);
        }
    }

    public class PostingContextDesignFactory : IDesignTimeDbContextFactory<DotNetCoreArchitectureContext>
    {
        public DotNetCoreArchitectureContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DotNetCoreArchitectureContext>()
                .UseNpgsql("User ID=postgres;Password=password;Host=dotnetcorearchitecture.sql;Port=5432;Database=dotnetcorearchitecture;Pooling=true;");

            return new DotNetCoreArchitectureContext(optionsBuilder.Options);
        }
    }
}
