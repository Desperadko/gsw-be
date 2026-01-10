using Azure.Core;
using GSW_Core.DTOs.Account;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests.Account;
using GSW_Core.Responses;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Errors.Exceptions;
using GSW_Core.Utilities.Helpers;
using GSW_Data.Constants;
using GSW_Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IPasswordHasher<Account> passwordHasher;

        public AccountService(
            IAccountRepository accountRepository,
            IPasswordHasher<Account> passwordHasher)
        {
            this.accountRepository = accountRepository;
            this.passwordHasher = passwordHasher;
        }

        public AccountDTO GetCurrent(IEnumerable<Claim> claims)
        {
            var usernameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name) ?? throw new BadRequestException("Invalid claims. No username set.");
            var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email) ?? throw new BadRequestException("Invalid claims. No email set.");
            var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role) ?? throw new BadRequestException("Invalid claims. No role set.");

            return new AccountDTO(usernameClaim.Value, emailClaim.Value, roleClaim.Value);
        }

        public async Task<AccountDTO> GetAsync(string username)
        {
            var account = await accountRepository.GetByUsernameAsync(username);

            if (account == null) throw new NotFoundException(ErrorFieldConstants.USERNAME, $"Account with username: {username}, does not exist.");

            return new AccountDTO(account.Username, account.Email, account.Role); 
        }

        public async Task<AccountDTO> GetAsync(int id)
        {
            var account = await accountRepository.GetByIdAsync(id);

            if (account == null) throw new NotFoundException(ErrorFieldConstants.ID, $"Account with id: '{id}', does not exist.");

            return new AccountDTO(account.Username, account.Email, account.Role);
        }

        public async Task<(int accountId, AccountDTO accountDTO)> RegisterAsync(AccountRegisterDTO credentials)
        {
            var account = new Account
            {
                Username = credentials.Username,
                Email = credentials.Email,
                Role = RoleHelper.User
            };

            var dto = new AccountDTO(account.Username, account.Email, account.Role);

            account.Password = passwordHasher.HashPassword(account, credentials.Password);

            if (!account.IsVaild) throw new BadRequestException("Something went wrong when hashing the password.");

            var count = await accountRepository.AddAsync(account);
            if (count <= 0) throw new BadRequestException("Couldn't add account to database.");

            return (account.Id, dto);
        }

        public async Task<(int accountId, AccountDTO accountDTO)> LoginAsync(AccountLoginDTO credentials)
        {
            var account = await accountRepository.GetByUsernameAsync(credentials.Username)
                ?? throw new NotFoundException(ErrorFieldConstants.USERNAME, $"Username: {credentials.Username} doesn't exists.");
            
            var isVerified = await VerifyPassword(account, credentials.Password);
            if (!isVerified)
            {
                throw new UnauthorizedException(ErrorFieldConstants.PASSWORD, "Invalid password.");
            }

            var dto = new AccountDTO(account.Username, account.Email, account.Role);

            return (account.Id, dto);
        }

        public async Task<AccountDTO> UpdateRoleAsync(int id, string role)
        {
            var lowerCasedRole = role.ToLower();
            if (!RoleHelper.IsValidRole(lowerCasedRole)) throw new BadRequestException($"Role: '{role}' doesn't exist");

            var account = await accountRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ErrorFieldConstants.ID, $"Account with id: '{id}' doesn't exist.");

            account.Role = lowerCasedRole;

            var count = await accountRepository.SaveChangesAsync();
            if (count <= 0) throw new BadRequestException($"Couldn't update role to account with username: '{account.Username}'");

            return new AccountDTO(account.Username, account.Email, account.Role);
        }

        private async Task<bool> VerifyPassword(Account account, string providedPassword)
        {
            var result = passwordHasher.VerifyHashedPassword(account, account.Password, providedPassword);

            switch (result)
            {
                case PasswordVerificationResult.Failed:
                    return false;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    account.Password = passwordHasher.HashPassword(account, providedPassword);
                    await accountRepository.SaveChangesAsync();
                    break;
            }

            return true;
        }
    }
}
