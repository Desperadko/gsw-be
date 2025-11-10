using GSW_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<int> AddAsync(RefreshToken token);
        Task<bool> IsValidAsync(string token);
        Task<bool> IsValidAsync(int accountId);
        Task<RefreshToken?> GetAsync(string token);
        Task<RefreshToken?> GetLastAsync(int accountId);
        Task<IEnumerable<RefreshToken>?> GetAllByAccountIdAsync(int accountId);
        Task<IEnumerable<RefreshToken>?> GetAllValidByAccountIdAsync(int accountId);
        Task<int> SaveAsync();
    }
}
