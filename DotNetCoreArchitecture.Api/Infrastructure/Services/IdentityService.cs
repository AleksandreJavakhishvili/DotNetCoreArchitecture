using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetCoreArchitecture.Api.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DotNetCoreArchitecture.Api.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public Guid GetUserIdentity()
        {
            return Guid.NewGuid();

            var user = _context.HttpContext.User;
            var sub = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(sub, out var result))
            {
                throw new DotNetCoreArchitecturePresentationException($"Invalid id specified, cannot parse to guid {sub}");
            }

            return result;
        }
    }
}
