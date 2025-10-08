using GSW_Core.DTOs.Account;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Requests;
using GSW_Core.Responses;
using GSW_Core.Services.Interfaces;
using GSW_Data.Models;
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

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> Login(LoginRequest dto)
        {
            throw new NotImplementedException();
        }
    }
}
