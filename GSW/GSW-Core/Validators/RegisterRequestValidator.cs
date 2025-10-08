using FluentValidation;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests;
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
        private readonly IAccountRepository accountRepository;

        public RegisterRequestValidator(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;

            RuleFor(a => a.Username)
                .NotEmpty()
                .WithMessage("Username should not be empty.")
                .MustAsync(async (username, cancellationToken) => !(await this.accountRepository.UsernameExists(username)))
                .WithMessage("Username either already exists or is left empty.");

            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("Email either already exists or is left empty.")
                .MustAsync(async (email, cancellationToken) => !(await this.accountRepository.EmailExists(email)))
                .WithMessage("Email should not be empty.");
        }
    }
}
