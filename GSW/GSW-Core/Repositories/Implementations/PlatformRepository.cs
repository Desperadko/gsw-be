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
    public class PlatformRepository : IPlatformRepository
    {
        private readonly GSWDbContext dbContext;

        public PlatformRepository(GSWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddAsync(Platform platform)
        {
            await dbContext.Platforms.AddAsync(platform);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Platform>?> GetAllAsync()
        {
            return await dbContext.Platforms
                .OrderBy(p => p.Name.ToLower())
                .ToListAsync();
        }

        public async Task<Platform?> GetAsync(int id)
        {
            return await dbContext.Platforms
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
