using GSW_Core.Repositories.Interfaces;
using GSW_Data;
using GSW_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Implementations
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly GSWDbContext dbContext;

        public RefreshTokenRepository(GSWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Add(RefreshToken token)
        {
            await dbContext.RefreshTokens.AddAsync(token);
            var count = await dbContext.SaveChangesAsync();
            
            return count;
        }

        public async Task<bool> IsValid(string token)
        {
            return await dbContext.RefreshTokens.AnyAsync(
                rt => rt.Token == token
                && !rt.IsRevoked
                && !rt.IsExpired);
        }

        public async Task<RefreshToken?> Get(string token)
        {
            return await dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<IEnumerable<RefreshToken>?> GetAllByAccountId(int accountId)
        {
            return await dbContext.RefreshTokens
                .Where(rt => rt.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<int> Save()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
