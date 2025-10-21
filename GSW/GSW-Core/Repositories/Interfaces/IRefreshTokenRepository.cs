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
        Task<int> Add(RefreshToken token);
        Task<bool> IsValid(string token);
        Task<bool> IsValid(int accountId);
        Task<RefreshToken?> Get(string token);
        Task<RefreshToken?> GetLast(int accountId);
        Task<IEnumerable<RefreshToken>?> GetAllByAccountId(int accountId);
        Task<int> Save();
    }
}
