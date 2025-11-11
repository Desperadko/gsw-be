using FluentValidation;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(a => a.Credentials.Username)
                .NotEmpty()
                .WithMessage("Username should not be empty.");

            RuleFor(a => a.Credentials.Email)
                .NotEmpty()
                .WithMessage("Email should not be empty.");

            RuleFor(a => a.Credentials.Password)
                .NotEmpty()
                .WithMessage("Password should not be empty.");
        }
    }
}
