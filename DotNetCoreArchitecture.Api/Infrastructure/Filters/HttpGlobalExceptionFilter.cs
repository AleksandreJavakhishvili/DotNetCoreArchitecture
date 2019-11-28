using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using DotNetCoreArchitecture.Application.Exceptions;
using DotNetCoreArchitecture.Domain.Exceptions;
using DotNetCoreArchitecture.Persistence.Exceptions;
using DotNetCoreArchitecture.Api.Infrastructure.ActionResults;
using DotNetCoreArchitecture.Api.Infrastructure.Exceptions;

namespace DotNetCoreArchitecture.Api.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var exceptionId = exception.HResult;

            _logger.LogError(new EventId(exception.HResult),
                exception,
                exception.Message);

            switch (exception)
            {
                case ForbiddenOperationException _:
                    {
                        var problemDetails = new ErrorResponse()
                        {
                            Status = StatusCodes.Status403Forbidden,
                            Message = exception.Message
                        };

                        context.Result = new ObjectResult(problemDetails);//not content?
                        context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        break;
                    }
                case ConflictingOperationException _:
                    {
                        var problemDetails = new ErrorResponse()
                        {
                            Status = StatusCodes.Status409Conflict,
                            Message = exception.Message
                        };

                        context.Result = new ConflictObjectResult(problemDetails);//not content?
                        context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                        break;
                    }
                case NotFoundException _:
                    {
                        var problemDetails = new ErrorResponse()
                        {
                            Status = StatusCodes.Status404NotFound,
                            Message = exception.Message
                        };

                        context.Result = new NotFoundObjectResult(problemDetails);//not content?
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    }
                case CommandValidatorException e when e.InnerException is ValidationException ex:
                    {
                        var problemDetails = new ValidationProblemDetails()
                        {
                            Instance = context.HttpContext.Request.Path,
                            Status = StatusCodes.Status400BadRequest,
                            Detail = "Please refer to the errors property for additional details."
                        };

                        problemDetails.Errors.Add("DomainValidations", ex.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToArray());

                        context.Result = new BadRequestObjectResult(problemDetails);
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    }
                case DotNetCoreArchitecturePresentationException _:
                case DotNetCoreArchitectureApplicationException _:
                case DotNetCoreArchitecturePersistenceException _:
                case DotNetCoreArchitectureDomainException _:
                    {
                        var problemDetails = new ValidationProblemDetails()
                        {
                            Instance = context.HttpContext.Request.Path,
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "Please refer to the errors property for additional details."
                        };

                        problemDetails.Errors.Add("DomainValidations", new string[] { exception.Message });

                        context.Result = new UnprocessableEntityObjectResult(problemDetails);
                        context.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                        break;
                    }
                default:
                    var json = new ErrorResponse
                    {
                        Message = $"An error has occured. Please refer to support with this id {exceptionId}.",
                        Status = StatusCodes.Status500InternalServerError
                    };

                    if (_env.IsDevelopment())
                    {
                        json.DeveloperMessage = context.Exception;
                    }

                    context.Result = new InternalServerErrorObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }


            context.ExceptionHandled = true;
        }


        public class ErrorResponse
        {
            public int Status { get; set; }
            public string Message { get; set; }
            public object DeveloperMessage { get; set; }
        }
    }
}
