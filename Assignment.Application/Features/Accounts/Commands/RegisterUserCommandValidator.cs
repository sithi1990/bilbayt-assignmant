using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Application.Features.Accounts.Commands
{

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Password should not be empty");
            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("User name should not be empty")
                .EmailAddress().WithMessage("Please enter valid email address");
            RuleFor(v => v.FullName)
                .NotEmpty().WithMessage("Full name should not be empty");

        }
    }

}
