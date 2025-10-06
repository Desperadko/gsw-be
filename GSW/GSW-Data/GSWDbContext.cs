using GSW_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GSW_Data
{
    public sealed class GSWDbContext : DbContext
    {
        public GSWDbContext(DbContextOptions<GSWDbContext> options) : base(options) { }

        public DbSet<TestModel> Tests { get; set; } = null!;
    }
}
