﻿using FluentValidation;
using Seller.Application.Commands;
using System.Collections.Generic;
using System.Linq;
using LibraryConstant = Seller.Application.Constants.Constants;

namespace Seller.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("ProductName is required")
                .NotNull().WithMessage("ProductName should not be null")
                .MaximumLength(30).WithMessage("ProductName must not exceed 30 characters.")
                .MinimumLength(5).WithMessage("ProductName must be above 5 characters");

            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("FirstName is required")
               .NotNull().WithMessage("FirstName should not be null")
               .MaximumLength(30).WithMessage("FirstName must not exceed 30 characters.")
               .MinimumLength(5).WithMessage("FirstName must be above 5 characters");

            RuleFor(p=> p.LastName)
               .NotEmpty().WithMessage("LastName is required")
               .NotNull().WithMessage("LastName should not be null")
               .MaximumLength(25).WithMessage("LastName must not exceed 25 characters.")
               .MinimumLength(3).WithMessage("LastName must be above 3 characters");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("Phone Number is required")
                .NotNull().WithMessage("Phone Number cannot be null")
                .MinimumLength(10).WithMessage("Minimum of 10 characters are required")
                .MaximumLength(10).WithMessage("Maximum of 10 characters are required")
                .MatchPhoneNumber();

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("Email is required.")
               .NotNull().WithMessage("Email should not be null.")
               .MatchEmail();

            RuleFor(p => p.Category)
               .NotEmpty().WithMessage("Category is required.")
               .NotNull().WithMessage("Category should not be null.")
               .Must(ValidCategory).WithMessage("Category must be one among the following: Painting/Sculptor/Ornament and case sensitive");
        }

        private bool ValidCategory(string category)
        {
            List<string> categoryList = LibraryConstant.CategoryList;
            return categoryList.Any(c=> c.Equals(category));
        }

    }

    public static class Extensions
    {
        public static IRuleBuilderOptions<T, string> MatchPhoneNumber<T>(this IRuleBuilder<T, string> rule)
          => rule.Matches(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)").WithMessage("Invalid phone number");

        public static IRuleBuilderOptions<T, string> MatchEmail<T>(this IRuleBuilder<T, string> rule)
          => rule.Matches("^(?:(?!.*?[.]{2})[a-zA-Z0-9](?:[a-zA-Z0-9.+!%-]{1,64}|)|\"[a-zA-Z0-9.+!% -]{1,64}\")@[a-zA-Z0-9][a-zA-Z0-9.-]+(.[a-z]{2,}|.[0-9]{1,})$").WithMessage("Invalid Email Address");
    }
}
