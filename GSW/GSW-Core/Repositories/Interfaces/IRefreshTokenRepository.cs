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
        Task<RefreshToken?> Get(string token);
        Task<IEnumerable<RefreshToken>?> GetAllByAccountId(int accountId);
        Task<int> Save();
    }
}
