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
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly GSWDbContext dbContext;

        public DeveloperRepository(GSWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddAsync(Developer developer)
        {
            await dbContext.Developers.AddAsync(developer);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Developer>?> GetAllAsync()
        {
            return await dbContext.Developers
                .OrderBy(developer => developer.Name, StringComparer.OrdinalIgnoreCase)
                .ToListAsync();
        }

        public async Task<Developer?> GetAsync(int id)
        {
            return await dbContext.Developers
                .Include(d => d.Products)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
