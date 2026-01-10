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
        Task<AccountDTO> GetAsync(string username);
        Task<AccountDTO> GetAsync(int id);
        Task<(int accountId, AccountDTO accountDTO)> RegisterAsync(AccountRegisterDTO credentials);
        Task<(int accountId, AccountDTO accountDTO)> LoginAsync(AccountLoginDTO credentials);
        Task<AccountDTO> UpdateRoleAsync(int id, string role);
    }
}
