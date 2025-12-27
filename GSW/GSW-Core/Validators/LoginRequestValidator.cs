using FluentValidation;
using GSW_Core.DTOs.Account;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(a => a.Credentials.Username)
                .NotEmpty()
                .WithMessage("Username should not be empty.")
                .OverridePropertyName(nameof(LoginRequest.Credentials.Username));

            RuleFor(a => a.Credentials.Password)
                .NotEmpty()
                .WithMessage("Password should not be empty.")
                .WithName(nameof(LoginRequest.Credentials.Password))
                .OverridePropertyName(nameof(LoginRequest.Credentials.Password));
        }
    }
}
