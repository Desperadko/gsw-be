using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> AddAsync(int accountId, string token, DateTime? expiresAt = null);
        Task<bool> ValidateAsync(string token);
        Task<bool> ValidateLastAsync(int accountId);
        Task RevokeAsync(string token);
        Task RevokeLastAsync(int accountId);
        Task RevokeAllAsync(int accountId);
    }
}
