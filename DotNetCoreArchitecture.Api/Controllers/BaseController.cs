using Microsoft.AspNetCore.Mvc;
using System;
using DotNetCoreArchitecture.Api.Infrastructure.Services;

namespace DotNetCoreArchitecture.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IIdentityService _identityService;

        protected BaseController(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        protected Guid ExtractUserFromToken()
        {
            return _identityService.GetUserIdentity();
        }

    }
}
