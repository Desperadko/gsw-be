using GSW_Core.DTOs.Account;
using GSW_Core.Requests;
using GSW_Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResponse> Register(RegisterRequest registerRequest);
        Task<LoginResponse> Login(LoginRequest dto);
    }
}
