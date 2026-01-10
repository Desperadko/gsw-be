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

        public async Task<int> AddAsync(RefreshToken token)
        {
            await dbContext.RefreshTokens.AddAsync(token);
            var count = await dbContext.SaveChangesAsync();
            
            return count;
        }

        public async Task<bool> IsValidAsync(string token)
        {
            return await dbContext.RefreshTokens.AnyAsync(
                rt => rt.Token == token
                && !rt.IsRevoked
                && rt.ExpiresAt > DateTime.UtcNow);
        }

        public async Task<bool> IsValidAsync(int accountId)
        {
            return await dbContext.RefreshTokens
                .Where(rt => rt.AccountId == accountId)
                .OrderByDescending(rt => rt.CreatedAt)
                .Select(rt => !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefaultAsync();
        }

        public async Task<RefreshToken?> GetAsync(string token)
        {
            return await dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<RefreshToken?> GetLastAsync(int accountId)
        {
            return await dbContext.RefreshTokens
                .Where(rt => rt.AccountId == accountId)
                .OrderByDescending(rt => rt.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RefreshToken>?> GetAllByAccountIdAsync(int accountId)
        {
            return await dbContext.RefreshTokens
                .Where(rt => rt.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RefreshToken>?> GetAllValidByAccountIdAsync(int accountId)
        {
            return await dbContext.RefreshTokens
                .Where(rt => rt.AccountId == accountId
                    && !rt.IsRevoked
                    && rt.ExpiresAt > DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
