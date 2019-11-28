using DotNetCoreArchitecture.Api.Infrastructure.Services;
using DotNetCoreArchitecture.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Api.Controllers
{
    [Route("api/v1/posts")]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator, IIdentityService identityService) : base(identityService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] CreatePostCommand command)
        {
            var result = await _mediator.Send(new CreatePostCommand
            {
                Name = command.Name,
                UserId = ExtractUserFromToken(),
                CommandId = Guid.NewGuid()
            });

            return Ok(result);
        }

    }
}
