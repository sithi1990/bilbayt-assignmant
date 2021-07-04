using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Application.Features.Accounts.Commands
{

    public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(v => v.Password)
                .NotNull().NotEmpty().WithMessage("Password should not be empty");
            RuleFor(v => v.UserName)
                .NotNull().NotEmpty().WithMessage("User name should not be empty");

        }
    }

}
