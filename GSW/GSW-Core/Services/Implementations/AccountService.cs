using GSW_Core.DTOs.Account;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests;
using GSW_Core.Responses;
using GSW_Core.Services.Interfaces;
using GSW_Core.Utilities.Constants;
using GSW_Core.Utilities.Errors.Exceptions;
using GSW_Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IJwtService jwtService;
        private readonly IPasswordHasher<Account> passwordHasher;

        public AccountService(
            IAccountRepository accountRepository,
            IJwtService jwtService,
            IPasswordHasher<Account> passwordHasher)
        {
            this.accountRepository = accountRepository;
            this.jwtService = jwtService;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Account> Get(string username)
        {
            var account = await accountRepository.GetByUsername(username);

            if (account == null) throw new NotFoundException($"Account with username: {username}, does not exist.");

            return account;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var dto = new AccountDTO()
            {
                Username = request.Username,
                Email = request.Email
            };
            
            var account = new Account
            {
                Username = dto.Username,
                Email = dto.Email,
                Role = RoleConstants.User
            };

            account.Password = passwordHasher.HashPassword(account, request.Password);

            if (!account.IsVaild) throw new BadRequestException("Something went wrong when hashing the password.");

            var count = await accountRepository.Add(account);
            if (count <= 0) throw new BadRequestException("Couldn't add account to database.");

            var token = jwtService.GenerateToken(account);
            
            return new RegisterResponse
            {
                Token = token,
                Account = dto
            };
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var account = await accountRepository.GetByUsername(request.Username) ?? throw new NotFoundException($"Username: {request.Username} doesn't exists.");
            
            var result = passwordHasher.VerifyHashedPassword(account, account.Password, request.Password);

            switch (result)
            {
                case PasswordVerificationResult.Failed:
                    throw new NotFoundException("Invalid password.");
                case PasswordVerificationResult.SuccessRehashNeeded:
                    account.Password = passwordHasher.HashPassword(account, request.Password);
                    await accountRepository.SaveChanges();
                    break;

            }

            var token = jwtService.GenerateToken(account);

            var dto = new AccountDTO
            {
                Username = account.Username,
                Email = account.Email,
            };

            var response = new LoginResponse
            {
                Token = token,
                Account = dto,
            };

            return response;
        }
    }
}
