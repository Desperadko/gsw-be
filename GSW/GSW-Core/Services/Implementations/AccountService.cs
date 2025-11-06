using Azure.Core;
using GSW_Core.DTOs.Account;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests.Account;
using GSW_Core.Responses;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using GSW_Core.Utilities.Errors.Exceptions;
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

            return new AccountDTO
            {
                Username = usernameClaim.Value,
                Email = emailClaim.Value,
                Role = roleClaim.Value
            };
        }

        public async Task<AccountDTO> Get(string username)
        {
            var account = await accountRepository.GetByUsername(username);

            if (account == null) throw new NotFoundException(ErrorFieldConstants.USERNAME, $"Account with username: {username}, does not exist.");

            return new AccountDTO { Username = account.Username, Email = account.Email, Role = account.Role }; 
        }

        public async Task<AccountDTO> Get(int id)
        {
            var account = await accountRepository.GetById(id);

            if (account == null) throw new NotFoundException(ErrorFieldConstants.ID, $"Account with id: '{id}', does not exist.");

            return new AccountDTO { Username = account.Username, Email = account.Email, Role = account.Role };
        }

        public async Task<(int accountId, AccountDTO accountDTO)> Register(RegisterRequest request)
        {
            var account = new Account
            {
                Username = request.Username,
                Email = request.Email,
                Role = RoleConstants.User
            };

            var dto = new AccountDTO()
            {
                Username = account.Username,
                Email = account.Email,
                Role = account.Role
            };

            account.Password = passwordHasher.HashPassword(account, request.Password);

            if (!account.IsVaild) throw new BadRequestException("Something went wrong when hashing the password.");

            var count = await accountRepository.Add(account);
            if (count <= 0) throw new BadRequestException("Couldn't add account to database.");

            return (account.Id, dto);
        }

        public async Task<(int accountId, AccountDTO accountDTO)> Login(LoginRequest request)
        {
            var account = await accountRepository.GetByUsername(request.Username)
                ?? throw new NotFoundException(ErrorFieldConstants.USERNAME, $"Username: {request.Username} doesn't exists.");
            
            var isVerified = await VerifyPassword(account, request.Password);
            if (!isVerified)
            {
                throw new UnauthorizedException(ErrorFieldConstants.PASSWORD, "Invalid password.");
            }

            var dto = new AccountDTO
            {
                Username = account.Username,
                Email = account.Email,
                Role = account.Role
            };

            return (account.Id, dto);
        }

        public async Task<AccountDTO> UpdateRole(int id, UpdateRoleRequest request)
        {
            var account = await accountRepository.GetById(id)
                ?? throw new NotFoundException(ErrorFieldConstants.ID, $"Account with id: '{id}' doesn't exist.");

            account.Role = request.Role;

            var count = await accountRepository.SaveChanges();
            if (count <= 0) throw new BadRequestException($"Couldn't update role to account with username: '{account.Username}'");

            return new AccountDTO { Username = account.Username, Email =  account.Email, Role = account.Role };
        }

        public async Task<bool> VerifyPassword(Account account, string providedPassword)
        {
            var result = passwordHasher.VerifyHashedPassword(account, account.Password, providedPassword);

            switch (result)
            {
                case PasswordVerificationResult.Failed:
                    return false;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    account.Password = passwordHasher.HashPassword(account, providedPassword);
                    await accountRepository.SaveChanges();
                    break;
            }

            return true;
        }
    }
}
