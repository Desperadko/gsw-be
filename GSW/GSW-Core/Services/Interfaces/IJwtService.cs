using GSW_Core.DTOs.Account;
using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(int accountId, AccountDTO accountDTO);
        string GenerateRefreshToken(int accountId);
        ClaimsPrincipal ValidateAccessTokenStructure(string token);
    }
}
