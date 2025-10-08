using GSW_Core.Repositories.Interfaces;
using GSW_Data;
using GSW_Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Repositories.Implementations
{
    public class TestRepository : ITestRepository
    {
        private readonly GSWDbContext dbContext;

        public TestRepository(GSWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<TestModel?> GetTestRecord(int id)
        {
            return dbContext.Tests.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<int> AddTestRecord(int someValue)
        {
            await dbContext.Tests.AddAsync(new TestModel() { SomeString = $"{someValue.GetHashCode()}" });
            return await dbContext.SaveChangesAsync();
        }
    }
}
