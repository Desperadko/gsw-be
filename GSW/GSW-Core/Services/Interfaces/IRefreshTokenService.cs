using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> Add(int accountId, string token, DateTime? expiresAt = null);
        Task<bool> Validate(string token);
        Task<bool> ValidateLast(int accountId);
        Task Revoke(string token);
        Task RevokeLast(int accountId);
        Task RevokeAll(int accountId);
    }
}
