using FluentValidation;
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
            RuleFor(a => a.Username)
                .NotEmpty()
                .WithMessage("Username should not be empty.");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("Password should not be empty.");
        }
    }
}
