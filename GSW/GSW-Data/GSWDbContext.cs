using GSW_Data.ModelConfigurations;
using GSW_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GSW_Data
{
    public sealed class GSWDbContext : DbContext
    {
        public GSWDbContext(DbContextOptions<GSWDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        }
    }
}
