using DotNetCoreArchitecture.Application.Commands;
using FluentValidation;

namespace ArithMath.Api.ApplicationLayer.Validations
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
           
        }
    }
}
