using DotNetCoreArchitecture.Application.Interfaces;
using DotNetCoreArchitecture.Domain.Aggregates.PostAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application.Commands
{
    public class CreatePostCommandHandler : AsyncRequestHandler<CreatePostCommand>
    {
        private readonly IRepository<Post> _repository;

        public CreatePostCommandHandler(IRepository<Post> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected async override Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post(request.Name);

            await _repository.AddAsync(post);
        }
    }
}
