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
    public class GenreRepository : IGenreRepository
    {
        private readonly GSWDbContext dbContext;

        public GenreRepository(GSWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddAsync(Genre genre)
        {
            await dbContext.Genres.AddAsync(genre);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await dbContext.Genres
                .AnyAsync(g => g.Name == name);
        }

        public async Task<IEnumerable<Genre>?> GetAllAsync()
        {
            return await dbContext.Genres
                .OrderBy(g => g.Name.ToLower())
                .ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await dbContext.Genres
                .Include(g => g.Products)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre?> GetByNameAsync(string name)
        {
            return await dbContext.Genres
                .Include(g => g.Products)
                .FirstOrDefaultAsync(g => g.Name == name);
        }
    }
}
