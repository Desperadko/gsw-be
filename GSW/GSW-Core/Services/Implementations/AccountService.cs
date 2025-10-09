using GSW_Core.DTOs.Account;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests;
using GSW_Core.Responses;
using GSW_Core.Services.Interfaces;
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
        private readonly IPasswordHasher<Account> passwordHasher;

        public AccountService(
            IAccountRepository accountRepository,
            IPasswordHasher<Account> passwordHasher)
        {
            this.accountRepository = accountRepository;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Account> Get(string username)
        {
            return await accountRepository.GetByUsername(username);
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
            };

            account.Password = passwordHasher.HashPassword(account, request.Password);

            //token aqcuisition

            var response = new RegisterResponse
            {
                Token = "",
                Account = dto,
            };

            if (!account.IsVaild)
            {
                response.Error = "Something went wrong with hashing the password.";
                return response;
            }

            var count = await accountRepository.Add(account);
            if(count <= 0)
            {
                response.Error = "Couldn't add account to database.";
                return response;
            }

            return response;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var account = await accountRepository.GetByUsername(request.Username);

            if(account == null)
            {
                return new LoginResponse
                {
                    Token = "",
                    Account = new AccountDTO
                    {
                        Email = "",
                        Username = request.Username,
                    },
                    Error = $"No account with username {request.Username} exists."
                };
            }

            var result = passwordHasher.VerifyHashedPassword(account, account.Password, request.Password);

            switch (result)
            {
                case PasswordVerificationResult.Failed:
                    return new LoginResponse
                    {
                        Token = "",
                        Account = new AccountDTO
                        {
                            Email = "",
                            Username = request.Username,
                        },
                        Error = "Password doesn't exist."
                    };
                case PasswordVerificationResult.SuccessRehashNeeded:
                    account.Password = passwordHasher.HashPassword(account, request.Password);
                    await accountRepository.SaveChanges();
                    break;

            }

            var dto = new AccountDTO
            {
                Username = account.Username,
                Email = account.Email,
            };

            //token aqcuisition

            var response = new LoginResponse
            {
                Token = "",
                Account = dto,
            };

            return response;
        }
    }
}
