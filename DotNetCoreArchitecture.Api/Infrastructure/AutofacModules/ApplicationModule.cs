using Autofac;
using DotNetCoreArchitecture.Api.Infrastructure.Services;
using DotNetCoreArchitecture.Application.Interfaces;
using DotNetCoreArchitecture.Domain.Aggregates.PostAggregate;
using DotNetCoreArchitecture.Persistence;
using DotNetCoreArchitecture.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using Module = Autofac.Module;


namespace DotNetCoreArchitecture.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ApplicationModule(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        protected override void Load(ContainerBuilder builder)
        {

            #region PostRepository

            builder.RegisterType<SqlRepository<Post, Guid>>()
                .Named<IRepository<Post>>(nameof(SqlRepository<Post, Guid>))
                .InstancePerLifetimeScope();

            builder.RegisterDecorator<IRepository<Post>>((ctx, inner) =>
            {
                var context = ctx.Resolve<DotNetCoreArchitectureContext>();

                return new WithSaveRepositoryDecorator<Post>(
                    new DomainEventRepositoryDecorator<Post>(inner, context),
                    context);
            }, nameof(SqlRepository<Post, Guid>));

            #endregion

            builder.RegisterType<CommandStore>()
                .As<ICommandStore>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityService>()
                .As<IIdentityService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<HttpContextAccessor>()
               .As<IHttpContextAccessor>()
               .SingleInstance();

        }
    }
}
