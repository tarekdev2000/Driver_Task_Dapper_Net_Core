using FluentValidation;

namespace Api.Controllers.Validators
{
    public class AddDriverValidator : AbstractValidator<Driver>
    {
        public AddDriverValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.LastName)
            .NotNull().NotEmpty().WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.Email)
            .NotNull().EmailAddress().WithMessage("{PropertyName} is not valid Email");


        }
    }
}
