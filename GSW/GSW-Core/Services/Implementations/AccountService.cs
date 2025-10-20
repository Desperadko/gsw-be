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
using System.Security.Claims;
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

            if (account == null) throw new NotFoundException($"Account with username: {username}, does not exist.");

            return new AccountDTO { Username = account.Username, Email = account.Email, Role = account.Role }; 
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
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

            var token = jwtService.GenerateAccessToken(account);
            
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

            var token = jwtService.GenerateAccessToken(account);

            var dto = new AccountDTO
            {
                Username = account.Username,
                Email = account.Email,
                Role = account.Role
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
