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
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        private readonly IAccountRepository accountRepository;

        public LoginRequestValidator(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;

            RuleFor(a => a.Username)
                .NotEmpty()
                .WithMessage("Username should not be empty.")
                .MustAsync(async (username, cancellationToken) => await this.accountRepository.UsernameExists(username))
                .WithMessage("Username doesn't exist or is left empty.");
        }
    }
}
