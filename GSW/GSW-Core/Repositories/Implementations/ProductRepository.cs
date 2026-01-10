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
    public class ProductRepository : IProductRepository
    {
        private readonly GSWDbContext dbContext;

        public ProductRepository(GSWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddAsync(Product product)
        {
            dbContext.ChangeTracker.Clear();

            //this is required to ensure that we don't add
            //  new records of the complementary entities to the DB
            //we need to make sure though,
            //  that the entities already do exist before hand
            AttachComplementaryEntities(product);

            await dbContext.Products.AddAsync(product);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await dbContext.Products
                .AnyAsync(p => p.Name == name);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await dbContext.Products
                .Include(p => p.Developers)
                .Include(p => p.Publishers)
                .Include(p => p.Genres)
                .Include(p => p.Platforms)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetByName(string name)
        {
            return await dbContext.Products
                .Include(p => p.Developers)
                .Include(p => p.Publishers)
                .Include(p => p.Genres)
                .Include(p => p.Platforms)
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        private void AttachComplementaryEntities(Product product)
        {
            foreach (var dev in product.Developers ?? Enumerable.Empty<Developer>())
                dbContext.Entry(dev).State = EntityState.Unchanged;
            foreach (var pub in product.Publishers ?? Enumerable.Empty<Publisher>())
                dbContext.Entry(pub).State = EntityState.Unchanged;
            foreach (var genre in product.Genres ?? Enumerable.Empty<Genre>())
                dbContext.Entry(genre).State = EntityState.Unchanged;
            foreach (var platform in product.Platforms ?? Enumerable.Empty<Platform>())
                dbContext.Entry(platform).State = EntityState.Unchanged;
        }
    }
}
