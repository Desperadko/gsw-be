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
    public class PublisherRepository : IPublisherRepository
    {
        private readonly GSWDbContext dbContext;

        public PublisherRepository(GSWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddAsync(Publisher publisher)
        {
            await dbContext.Publishers.AddAsync(publisher);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Publisher>?> GetAllAsync()
        {
            return await dbContext.Publishers
                .OrderBy(p => p.Name.ToLower())
                .ToListAsync();
        }

        public async Task<Publisher?> GetAsync(int id)
        {
            return await dbContext.Publishers
                .Include(p => p.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
