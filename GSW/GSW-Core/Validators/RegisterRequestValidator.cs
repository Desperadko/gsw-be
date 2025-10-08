using FluentValidation;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        private readonly IAccountRepository accountRepository;

        public RegisterRequestValidator(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;

            RuleFor(a => a.Username)
                .MustAsync(async (username, cancellationToken) => await accountRepository.UsernameExists(username))
                .NotEmpty()
                .WithMessage("Username either already exists or is left empty.");

            RuleFor(a => a.Email)
                .MustAsync(async (email, cancellationToken) => await accountRepository.EmailExists(email))
                .NotEmpty()
                .WithMessage("Email either already exists or is left empty.");
        }
    }
}
