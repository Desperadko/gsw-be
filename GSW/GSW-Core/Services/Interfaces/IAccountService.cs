using GSW_Core.DTOs.Account;
using GSW_Core.Requests.Account;
using GSW_Core.Responses;
using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IAccountService
    {
        AccountDTO GetCurrent(IEnumerable<Claim> claims);
        Task<AccountDTO> Get(string username);
        Task<AccountDTO> Get(int id);
        Task<(int accountId, AccountDTO accountDTO)> Register(RegisterRequest request);
        Task<(int accountId, AccountDTO accountDTO)> Login(LoginRequest request);
        Task<AccountDTO> UpdateRole(int id, UpdateRoleRequest request);
    }
}
