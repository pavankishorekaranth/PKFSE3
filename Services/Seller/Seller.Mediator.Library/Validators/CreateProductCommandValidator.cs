using FluentValidation;
using Seller.Mediator.Library.Commands;

namespace Seller.Mediator.Library.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("{ProductName} is required")
                .NotNull().WithMessage("{ProductName} should not be null")
                .MaximumLength(30).WithMessage("{ProductName} must not exceed 30 characters.")
                .MinimumLength(5).WithMessage("{ProductName} must be above 5 characters");

            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{FirstName} is required")
               .NotNull().WithMessage("{FirstName} should not be null")
               .MaximumLength(30).WithMessage("{FirstName} must not exceed 30 characters.")
               .MinimumLength(5).WithMessage("{FirstName} must be above 5 characters");

            RuleFor(p=> p.LastName)
               .NotEmpty().WithMessage("{LastName} is required")
               .NotNull().WithMessage("{LastName} should not be null")
               .MaximumLength(25).WithMessage("{LastName} must not exceed 25 characters.")
               .MinimumLength(3).WithMessage("{LastName} must be above 3 characters");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("Phone Number is required")
                .NotNull().WithMessage("Phone Number cannot be null")
                .MatchPhoneNumber();

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{Email} is required.")
               .NotNull().WithMessage("{Email} should not be null.")
               .EmailAddress().WithMessage("Valid email address is required");

        }

        

    }

    public static class Extensions
    {
        public static IRuleBuilderOptions<T, string> MatchPhoneNumber<T>(this IRuleBuilder<T, string> rule)
          => rule.Matches(@"((\+*)((0[ -]*)*|((91 )*))((\d{12})+|(\d{10})+))|\d{5}([- ]*)\d{6}").WithMessage("Invalid phone number");
    }
}
